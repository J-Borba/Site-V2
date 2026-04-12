using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace projects_api.Data.Repositories;

public class UserRepository(UserManager<User> userManager, SignInManager<User> signInManager) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync() =>
        await userManager.Users.ToListAsync();

    public async Task<User?> GetByEmailAsync(string email) =>
        await userManager.FindByEmailAsync(email);

    public async Task<IdentityResult> CreateAsync(User user, string password) =>
        await userManager.CreateAsync(user, password);

    public Task<SignInResult> LoginAsync(User user, string password) =>
        signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: false);

    public async Task<IdentityResult> UpdateUserNameAsync(User user, string newUserName) =>
        await userManager.SetUserNameAsync(user, newUserName) ?? IdentityResult.Failed();

    public async Task<IdentityResult> UpdateEmailAsync(User user, string newEmail) =>
        await userManager.SetEmailAsync(user, newEmail) ?? IdentityResult.Failed();
}
