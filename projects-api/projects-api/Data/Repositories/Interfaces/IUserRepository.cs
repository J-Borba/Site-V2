using projects_api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace projects_api.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByEmailAsync(string email);
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<SignInResult> LoginAsync(User user, string password);
    Task<IdentityResult> UpdateUserNameAsync(User user, string newUserName);
    Task<IdentityResult> UpdateEmailAsync(User user, string newEmail);
}
