using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using projects_api.Data.Dtos.User;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services;
using projects_api.Services.Interfaces;
using Xunit;

namespace projects_api.Tests.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepo = new();
    private readonly Mock<IRefreshTokenRepository> _refreshRepo = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ITokenService> _tokenService = new();
    private readonly Mock<UserManager<User>> _userManager;
    private readonly UserService _sut;

    public UserServiceTests()
    {
        _userManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null!, null!, null!, null!, null!, null!, null!, null!);

        _sut = new UserService(_userRepo.Object,
                               _refreshRepo.Object,
                               _mapper.Object,
                               _tokenService.Object,
                               _userManager.Object);
    }

    [Fact]
    public async Task GetUsersAsync_ReturnsMappedDtos()
    {
        var users = new List<User>
        {
            new() { Id = "1" },
            new() { Id = "2" }
        };

        var dtos = new List<ReadUserDto>
        {
            new("1", "a", "a@a.com"),
            new("2", "b", "b@b.com")
        };

        _userRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(users);
        _mapper.Setup(m => m.Map<IEnumerable<ReadUserDto>>(users)).Returns(dtos);

        var result = await _sut.GetUsersAsync();

        result.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetUsersAsync_WhenNoUsers_ReturnsEmptyCollection()
    {
        _userRepo.Setup(r => r.GetAllAsync()).ReturnsAsync([]);
        _mapper.Setup(m => m.Map<IEnumerable<ReadUserDto>>(It.IsAny<IEnumerable<User>>()))
               .Returns([]);

        var result = await _sut.GetUsersAsync();

        result.Should().BeEmpty();
    }

    // CreateUserAsync

    [Fact]
    public async Task CreateUserAsync_WhenSucceeds_ReturnsValidResult()
    {
        var dto = new CreateUserDto("a@b.com", "Pass1234!", null);
        _mapper.Setup(m => m.Map<User>(dto)).Returns(new User());
        _userRepo.Setup(r => r.CreateAsync(It.IsAny<User>(), dto.Password))
                 .ReturnsAsync(IdentityResult.Success);

        var result = await _sut.CreateUserAsync(dto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public async Task CreateUserAsync_WhenIdentityFails_ReturnsErrors()
    {
        var dto = new CreateUserDto("a@b.com", "weak", null);
        _mapper.Setup(m => m.Map<User>(dto)).Returns(new User());
        _userRepo.Setup(r => r.CreateAsync(It.IsAny<User>(), dto.Password))
                 .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password too short." }));

        var result = await _sut.CreateUserAsync(dto);

        result.IsValid.Should().BeFalse();
        result.ErrorMessages.Should().Contain("Password too short.");
    }

    // LoginAsync

    [Fact]
    public async Task LoginAsync_WhenUserNotFound_ReturnsValidationError()
    {
        _userRepo.Setup(r => r.GetByEmailAsync("missing@test.com")).ReturnsAsync((User?)null);

        var result = await _sut.LoginAsync(new LoginUserDto("missing@test.com", "x"));

        result.Validation.IsValid.Should().BeFalse();
        result.AccessToken.Should().BeNullOrEmpty();
    }

    [Fact]
    public async Task LoginAsync_WhenPasswordInvalid_ReturnsValidationError()
    {
        var user = new User { Id = "1", Email = "a@b.com" };
        _userRepo.Setup(r => r.GetByEmailAsync("a@b.com")).ReturnsAsync(user);
        _userRepo.Setup(r => r.LoginAsync(user, "wrong")).ReturnsAsync(SignInResult.Failed);

        var result = await _sut.LoginAsync(new LoginUserDto("a@b.com", "wrong"));

        result.Validation.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task LoginAsync_WhenCredentialsValid_ReturnsTokens()
    {
        var user = new User { Id = "1", UserName = "alice", Email = "alice@test.com" };
        _userRepo.Setup(r => r.GetByEmailAsync(user.Email)).ReturnsAsync(user);
        _userRepo.Setup(r => r.LoginAsync(user, "Pass1234!")).ReturnsAsync(SignInResult.Success);

        _userManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(["User"]);

        _tokenService.Setup(t => t.GenerateRefreshToken(user.Id))
                     .Returns(("raw-refresh", new RefreshToken { TokenHash = "hash" }));
        _tokenService.Setup(t => t.GenerateAccessToken(user, It.IsAny<IList<string>>()))
                     .Returns("access-token");

        var result = await _sut.LoginAsync(new LoginUserDto(user.Email, "Pass1234!"));

        result.Validation.IsValid.Should().BeTrue();
        result.AccessToken.Should().Be("access-token");
        result.RefreshToken.Should().Be("raw-refresh");
        result.UserName.Should().Be("alice");
        result.Email.Should().Be("alice@test.com");
        _refreshRepo.Verify(r => r.CreateAsync(It.IsAny<RefreshToken>()), Times.Once);
    }

    // LogoutAsync

    [Fact]
    public async Task LogoutAsync_WhenTokenFoundAndNotRevoked_RevokesIt()
    {
        var stored = new RefreshToken { TokenHash = "hash", IsRevoked = false };
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync(stored);

        await _sut.LogoutAsync("raw");

        _refreshRepo.Verify(r => r.RevokeAsync(stored), Times.Once);
    }

    [Fact]
    public async Task LogoutAsync_WhenTokenNotFound_DoesNotCallRevoke()
    {
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync((RefreshToken?)null);

        await _sut.LogoutAsync("raw");

        _refreshRepo.Verify(r => r.RevokeAsync(It.IsAny<RefreshToken>()), Times.Never);
    }

    [Fact]
    public async Task LogoutAsync_WhenTokenAlreadyRevoked_DoesNotCallRevoke()
    {
        var stored = new RefreshToken { TokenHash = "hash", IsRevoked = true };
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync(stored);

        await _sut.LogoutAsync("raw");

        _refreshRepo.Verify(r => r.RevokeAsync(It.IsAny<RefreshToken>()), Times.Never);
    }

    // RefreshAsync

    [Fact]
    public async Task RefreshAsync_WhenTokenNotFound_ReturnsError()
    {
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync((RefreshToken?)null);

        var result = await _sut.RefreshAsync("raw");

        result.Validation.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task RefreshAsync_WhenTokenRevoked_ReturnsError()
    {
        var stored = new RefreshToken { TokenHash = "hash", IsRevoked = true, ExpiresAt = DateTimeOffset.UtcNow.AddDays(1) };
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync(stored);

        var result = await _sut.RefreshAsync("raw");

        result.Validation.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task RefreshAsync_WhenTokenExpired_ReturnsError()
    {
        var stored = new RefreshToken { TokenHash = "hash", IsRevoked = false, ExpiresAt = DateTimeOffset.UtcNow.AddDays(-1) };
        _tokenService.Setup(t => t.HashToken("raw")).Returns("hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("hash")).ReturnsAsync(stored);

        var result = await _sut.RefreshAsync("raw");

        result.Validation.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task RefreshAsync_WhenValid_RevokesOldAndIssuesNew()
    {
        var user = new User { Id = "1", UserName = "alice", Email = "alice@test.com" };
        var stored = new RefreshToken
        {
            TokenHash = "old-hash",
            IsRevoked = false,
            ExpiresAt = DateTimeOffset.UtcNow.AddDays(7),
            UserId = user.Id,
            User = user
        };

        _tokenService.Setup(t => t.HashToken("raw-token")).Returns("old-hash");
        _refreshRepo.Setup(r => r.GetByHashAsync("old-hash")).ReturnsAsync(stored);
        _userManager.Setup(m => m.GetRolesAsync(user)).ReturnsAsync(["User"]);

        _tokenService.Setup(t => t.GenerateRefreshToken(user.Id))
                     .Returns(("new-raw", new RefreshToken { TokenHash = "new-hash" }));
        _tokenService.Setup(t => t.GenerateAccessToken(user, It.IsAny<IList<string>>()))
                     .Returns("new-access-token");

        var result = await _sut.RefreshAsync("raw-token");

        result.Validation.IsValid.Should().BeTrue();
        result.AccessToken.Should().Be("new-access-token");
        result.RefreshToken.Should().Be("new-raw");
        result.UserName.Should().Be("alice");
        result.Email.Should().Be("alice@test.com");
        _refreshRepo.Verify(r => r.RevokeAsync(stored), Times.Once);
        _refreshRepo.Verify(r => r.CreateAsync(It.IsAny<RefreshToken>()), Times.Once);
    }

    // UpdateCurrentUserAsync

    [Fact]
    public async Task UpdateCurrentUserAsync_WhenUserNotFound_ReturnsError()
    {
        _userRepo.Setup(r => r.GetByEmailAsync("missing@test.com")).ReturnsAsync((User?)null);

        var result = await _sut.UpdateCurrentUserAsync(new UpdateUserDto(null, null), "missing@test.com");

        result.IsValid.Should().BeFalse();
        result.ErrorMessages.Should().Contain("User not found.");
    }

    [Fact]
    public async Task UpdateCurrentUserAsync_WhenNewUserNameProvided_UpdatesIt()
    {
        var user = new User { UserName = "old", Email = "a@b.com" };
        _userRepo.Setup(r => r.GetByEmailAsync("a@b.com")).ReturnsAsync(user);
        _userRepo.Setup(r => r.UpdateUserNameAsync(user, "new")).ReturnsAsync(IdentityResult.Success);

        var result = await _sut.UpdateCurrentUserAsync(new UpdateUserDto("new", null), "a@b.com");

        result.IsValid.Should().BeTrue();
        _userRepo.Verify(r => r.UpdateUserNameAsync(user, "new"), Times.Once);
    }

    [Fact]
    public async Task UpdateCurrentUserAsync_WhenNewEmailProvided_UpdatesIt()
    {
        var user = new User { UserName = "alice", Email = "old@b.com" };
        _userRepo.Setup(r => r.GetByEmailAsync("old@b.com")).ReturnsAsync(user);
        _userRepo.Setup(r => r.UpdateEmailAsync(user, "new@b.com")).ReturnsAsync(IdentityResult.Success);

        var result = await _sut.UpdateCurrentUserAsync(new UpdateUserDto(null, "new@b.com"), "old@b.com");

        result.IsValid.Should().BeTrue();
        _userRepo.Verify(r => r.UpdateEmailAsync(user, "new@b.com"), Times.Once);
    }

    [Fact]
    public async Task UpdateCurrentUserAsync_WhenNoChangesProvided_ReturnsError()
    {
        var user = new User { UserName = "alice", Email = "a@b.com" };
        _userRepo.Setup(r => r.GetByEmailAsync("a@b.com")).ReturnsAsync(user);

        var result = await _sut.UpdateCurrentUserAsync(new UpdateUserDto(null, null), "a@b.com");

        result.IsValid.Should().BeFalse();
        result.ErrorMessages.Should().Contain("No changes were made.");
    }
}
