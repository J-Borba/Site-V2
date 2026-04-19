using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using projects_api.Data.Models;
using projects_api.Services;
using projects_api.Tests.Helpers;
using Xunit;

namespace projects_api.Tests.Services;

public class TokenServiceTests
{
    private readonly TokenService _sut = new(TestAppSettings.Create());

    // HashToken

    [Fact]
    public void HashToken_IsDeterministic()
    {
        var hash1 = _sut.HashToken("my-token");
        var hash2 = _sut.HashToken("my-token");

        hash1.Should().Be(hash2);
    }

    [Fact]
    public void HashToken_DifferentInputs_ProduceDifferentHashes()
    {
        _sut.HashToken("token-a").Should().NotBe(_sut.HashToken("token-b"));
    }

    [Fact]
    public void HashToken_ReturnsUppercaseHex64Chars()
    {
        var hash = _sut.HashToken("any-input");

        hash.Should().MatchRegex("^[0-9A-F]{64}$");
    }

    // GenerateRefreshToken

    [Fact]
    public void GenerateRefreshToken_RawTokenIsValidBase64()
    {
        var (rawToken, _) = _sut.GenerateRefreshToken("user-1");

        var act = () => Convert.FromBase64String(rawToken);
        act.Should().NotThrow();
    }

    [Fact]
    public void GenerateRefreshToken_EntityHashMatchesRawToken()
    {
        var (rawToken, entity) = _sut.GenerateRefreshToken("user-1");

        entity.TokenHash.Should().Be(_sut.HashToken(rawToken));
    }

    [Fact]
    public void GenerateRefreshToken_EntityHasCorrectUserId()
    {
        var (_, entity) = _sut.GenerateRefreshToken("user-42");

        entity.UserId.Should().Be("user-42");
    }

    [Fact]
    public void GenerateRefreshToken_EntityExpiresInApproximately7Days()
    {
        var before = DateTimeOffset.UtcNow.AddDays(7);
        var (_, entity) = _sut.GenerateRefreshToken("user-1");
        var after = DateTimeOffset.UtcNow.AddDays(7);

        entity.ExpiresAt.Should().BeOnOrAfter(before).And.BeOnOrBefore(after);
    }

    [Fact]
    public void GenerateRefreshToken_IsRevokedIsFalse()
    {
        var (_, entity) = _sut.GenerateRefreshToken("user-1");

        entity.IsRevoked.Should().BeFalse();
    }

    [Fact]
    public void GenerateRefreshToken_TwoCallsProduceDifferentTokens()
    {
        var (token1, _) = _sut.GenerateRefreshToken("user-1");
        var (token2, _) = _sut.GenerateRefreshToken("user-1");

        token1.Should().NotBe(token2);
    }

    // GenerateAccessToken

    private static User MakeUser(string id = "u1", string username = "alice", string email = "alice@test.com") =>
        new() { Id = id, UserName = username, Email = email };

    [Fact]
    public void GenerateAccessToken_ReturnsNonEmptyString()
    {
        var token = _sut.GenerateAccessToken(MakeUser(), []);

        token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void GenerateAccessToken_ContainsNameIdentifierClaim()
    {
        var user = MakeUser(id: "id-123");
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_sut.GenerateAccessToken(user, []));

        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.NameIdentifier && c.Value == "id-123");
    }

    [Fact]
    public void GenerateAccessToken_ContainsEmailClaim()
    {
        var user = MakeUser(email: "test@email.com");
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_sut.GenerateAccessToken(user, []));

        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Email && c.Value == "test@email.com");
    }

    [Fact]
    public void GenerateAccessToken_ContainsNameClaim()
    {
        var user = MakeUser(username: "bob");
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_sut.GenerateAccessToken(user, []));

        jwt.Claims.Should().Contain(c => c.Type == ClaimTypes.Name && c.Value == "bob");
    }

    [Fact]
    public void GenerateAccessToken_ContainsAllRoleClaims()
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(
            _sut.GenerateAccessToken(MakeUser(), ["Admin", "SuperAdmin"]));

        var roles = jwt.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
        roles.Should().Contain("Admin").And.Contain("SuperAdmin");
    }

    [Fact]
    public void GenerateAccessToken_HasCorrectIssuerAndAudience()
    {
        var settings = TestAppSettings.Create();
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_sut.GenerateAccessToken(MakeUser(), []));

        jwt.Issuer.Should().Be(settings.Secrets.JwtIssuer);
        jwt.Audiences.Should().Contain(settings.Secrets.JwtAudience);
    }

    [Fact]
    public void GenerateAccessToken_ExpiryIsApproximately15MinutesFromNow()
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(_sut.GenerateAccessToken(MakeUser(), []));
        var expected = DateTime.UtcNow.AddMinutes(15);

        // JWT exp is truncated to whole seconds, so allow ±2s tolerance
        jwt.ValidTo.Should().BeCloseTo(expected, TimeSpan.FromSeconds(2));
    }
}
