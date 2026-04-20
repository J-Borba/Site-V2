using projects_api.Common.Validation;
using projects_api.Data.Dtos.User;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace projects_api.Services;

public class UserService(IUserRepository repository,
                         IRefreshTokenRepository refreshTokenRepository,
                         IMapper mapper,
                         ITokenService tokenService,
                         UserManager<User> userManager) : IUserService
{
    public async Task<IEnumerable<ReadUserDto>> GetUsersAsync() =>
        mapper.Map<IEnumerable<ReadUserDto>>(await repository.GetAllAsync());

    public async Task<ValidationResult> CreateUserAsync(CreateUserDto dto)
    {
        var validation = new ValidationResult();
        var user = mapper.Map<User>(dto);

        var result = await repository.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            validation.AddErrors(result.Errors.Select(e => e.Description));

        return validation;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginUserDto dto)
    {
        var response = new LoginResponseDto();
        const string errorMsg = "Não foi possível validar as credenciais fornecidas.";

        var user = await repository.GetByEmailAsync(dto.Email);
        if (user is null)
        {
            response.Validation.AddError(errorMsg);
            return response;
        }

        var result = await repository.LoginAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            response.Validation.AddError(errorMsg);
            return response;
        }

        var roles = await userManager.GetRolesAsync(user);
        var (rawRefreshToken, refreshEntity) = tokenService.GenerateRefreshToken(user.Id);
        await refreshTokenRepository.CreateAsync(refreshEntity);

        response.AccessToken = tokenService.GenerateAccessToken(user, roles);
        response.RefreshToken = rawRefreshToken;
        response.UserName = user.UserName!;
        response.Email = user.Email!;
        response.Roles = roles;

        return response;
    }

    public async Task<LoginResponseDto> RefreshAsync(string rawToken)
    {
        var response = new LoginResponseDto();
        const string errorMsg = "Token inválido ou expirado.";

        var tokenHash = tokenService.HashToken(rawToken);
        var stored = await refreshTokenRepository.GetByHashAsync(tokenHash);

        if (stored is null || stored.IsRevoked || stored.ExpiresAt < DateTimeOffset.UtcNow)
        {
            response.Validation.AddError(errorMsg);
            return response;
        }

        await refreshTokenRepository.RevokeAsync(stored);

        var roles = await userManager.GetRolesAsync(stored.User);
        var (newRawToken, newEntity) = tokenService.GenerateRefreshToken(stored.UserId);
        await refreshTokenRepository.CreateAsync(newEntity);

        response.AccessToken = tokenService.GenerateAccessToken(stored.User, roles);
        response.RefreshToken = newRawToken;
        response.UserName = stored.User.UserName!;
        response.Email = stored.User.Email!;
        response.Roles = roles;

        return response;
    }

    public async Task LogoutAsync(string rawToken)
    {
        var tokenHash = tokenService.HashToken(rawToken);
        var stored = await refreshTokenRepository.GetByHashAsync(tokenHash);

        if (stored is not null && !stored.IsRevoked)
            await refreshTokenRepository.RevokeAsync(stored);
    }

    public async Task<ValidationResult> UpdateCurrentUserAsync(UpdateUserDto dto, string currentUserEmail)
    {
        var validation = new ValidationResult();
        var user = await repository.GetByEmailAsync(currentUserEmail);

        if (user is null)
        {
            validation.AddError("User not found.");
            return validation;
        }

        var changed = false;

        if (!string.IsNullOrEmpty(dto.NewUserName) && !string.Equals(user.UserName, dto.NewUserName))
        {
            var result = await repository.UpdateUserNameAsync(user, dto.NewUserName);
            if (!result.Succeeded) validation.AddErrors(result.Errors.Select(e => e.Description));
            else changed = true;
        }

        if (!string.IsNullOrEmpty(dto.NewEmail) && !string.Equals(user.Email, dto.NewEmail))
        {
            var result = await repository.UpdateEmailAsync(user, dto.NewEmail);
            if (!result.Succeeded) validation.AddErrors(result.Errors.Select(e => e.Description));
            else changed = true;
        }

        if (!changed && validation.IsValid)
            validation.AddError("No changes were made.");

        return validation;
    }
}
