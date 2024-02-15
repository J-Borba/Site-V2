using api.Data.Dtos.User;
using api.Validation;
using System.Security.Claims;

namespace api.Services.Interfaces;

public interface IUserService
{
    (ValidationResult, IEnumerable<ReadUserDto>) GetUsersAsync(IEnumerable<Claim> currentUserClaims);
    Task<ValidationResult> CreateUserAsync(CreateUserDto dto);
    Task<(ValidationResult, string)> LoginUserAsync(LoginUserDto dto);
    Task<ValidationResult> UpdateUserAsync(UpdateUserDto dto);
}
