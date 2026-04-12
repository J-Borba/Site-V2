using projects_api.Common.Validation;
using projects_api.Data.Dtos.User;

namespace projects_api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<ReadUserDto>> GetUsersAsync();
    Task<ValidationResult> CreateUserAsync(CreateUserDto dto);
    Task<(ValidationResult, string)> LoginAsync(LoginUserDto dto);
    Task<ValidationResult> UpdateCurrentUserAsync(UpdateUserDto dto, string currentUserEmail);
}
