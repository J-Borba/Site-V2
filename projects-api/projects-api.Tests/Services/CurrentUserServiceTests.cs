using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using projects_api.Services;
using Xunit;

namespace projects_api.Tests.Services;

public class CurrentUserServiceTests
{
    private readonly Mock<IHttpContextAccessor> _accessor = new();
    private readonly CurrentUserService _sut;

    public CurrentUserServiceTests()
    {
        _sut = new CurrentUserService(_accessor.Object);
    }

    private void SetupUser(params Claim[] claims)
    {
        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims));
        var context = new DefaultHttpContext { User = principal };
        _accessor.Setup(a => a.HttpContext).Returns(context);
    }

    [Fact]
    public void Id_WhenClaimPresent_ReturnsValue()
    {
        SetupUser(new Claim(ClaimTypes.NameIdentifier, "user-99"));

        _sut.Id.Should().Be("user-99");
    }

    [Fact]
    public void Email_WhenClaimPresent_ReturnsValue()
    {
        SetupUser(new Claim(ClaimTypes.Email, "test@test.com"));

        _sut.Email.Should().Be("test@test.com");
    }

    [Fact]
    public void UserName_WhenClaimPresent_ReturnsValue()
    {
        SetupUser(new Claim(ClaimTypes.Name, "alice"));

        _sut.UserName.Should().Be("alice");
    }

    [Fact]
    public void Roles_WhenMultipleRoleClaims_ReturnsAll()
    {
        SetupUser(new Claim(ClaimTypes.Role, "Admin"), new Claim(ClaimTypes.Role, "SuperAdmin"));

        _sut.Roles.Should().Contain("Admin").And.Contain("SuperAdmin");
    }

    [Fact]
    public void Id_WhenHttpContextIsNull_ReturnsEmptyString()
    {
        _accessor.Setup(a => a.HttpContext).Returns((HttpContext?)null);

        _sut.Id.Should().BeEmpty();
    }

    [Fact]
    public void Roles_WhenNoRoleClaims_ReturnsEmptyEnumerable()
    {
        SetupUser(new Claim(ClaimTypes.Email, "test@test.com"));

        _sut.Roles.Should().BeEmpty();
    }
}
