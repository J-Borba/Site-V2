using projects_api.Common.Validation;
using projects_api.Data.Dtos.User;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace projects_api.Services;

public class UserService(IUserRepository repository,
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

    public async Task<(ValidationResult, string)> LoginAsync(LoginUserDto dto)
    {
        var validation = new ValidationResult();
        const string errorMsg = "Invalid credentials.";

        var user = await repository.GetByEmailAsync(dto.Email);
        if (user is null)
        {
            validation.AddError(errorMsg);
            return (validation, string.Empty);
        }

        var result = await repository.LoginAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            validation.AddError(errorMsg);
            return (validation, string.Empty);
        }

        var roles = await userManager.GetRolesAsync(user);
        return (validation, tokenService.GenerateToken(user, roles));
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
