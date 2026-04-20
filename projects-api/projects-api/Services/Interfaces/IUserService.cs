using projects_api.Common.Validation;
using projects_api.Data.Dtos.User;

namespace projects_api.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<ReadUserDto>> GetUsersAsync();
    Task<ValidationResult> CreateUserAsync(CreateUserDto dto);
    Task<LoginResponseDto> LoginAsync(LoginUserDto dto);
    Task<LoginResponseDto> RefreshAsync(string rawToken);
    Task LogoutAsync(string rawToken);
    Task<ValidationResult> UpdateCurrentUserAsync(UpdateUserDto dto, string currentUserEmail);
}
