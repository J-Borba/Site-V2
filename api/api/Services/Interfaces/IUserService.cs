using api.Data.Dtos.User;
using api.Validation;

namespace api.Services.Interfaces;

public interface IUserService
{
    public Task<ValidationResult> CreateUserAsync(CreateUserDto dto);
    public Task<(ValidationResult, string)> LoginUserAsync(LoginUserDto dto);
    public Task<(ValidationResult, string)> LoginEmailUserAsync(LoginUserDto dto);
    public Task<List<UserDto>> GetUserAsync();
}
