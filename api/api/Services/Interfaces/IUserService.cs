using api.Common.Validation;
using api.Data.Dtos.User;

namespace api.Services.Interfaces;

public interface IUserService
{
    Task<(ValidationResult, IEnumerable<ReadUserDto>)> GetUsersAsync(string currentUserName);
    Task<ValidationResult> CreateUserAsync(CreateUserDto dto);
    Task<(ValidationResult, string)> LoginUserAsync(LoginUserDto dto);
    Task<ValidationResult> UpdateCurrentUserAsync(UpdateUserDto dto, string currentUserEmail);
}
