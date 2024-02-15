using api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace api.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<SignInResult> LoginUserAsync(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false);
    Task<IdentityResult> UpdateUserNameAsync(User user, string newUserName);
    Task<IdentityResult> UpdateUserEmailAsync(User user, string newEmail);

}
