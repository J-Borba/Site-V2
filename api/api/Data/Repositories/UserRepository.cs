using api.Data.Contexts;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repositories;

public class UserRepository : IUserRepository
{
    #region Dependency Injections
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserRepository(SiteDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    #endregion

    public async Task<IEnumerable<User>> GetAllAsync()
        => await _userManager.Users.ToListAsync();

    public async Task<User> GetUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
        => await _userManager.CreateAsync(user, password);

    public async Task<SignInResult> LoginUserAsync(string userName, string password, bool isPersistent = false, bool lockoutOnFailure = false)
        => await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

    public async Task<IdentityResult> UpdateUserNameAsync(User user, string newUserName)
        => await _userManager.SetUserNameAsync(user, newUserName);

    public async Task<IdentityResult> UpdateUserEmailAsync(User user, string newEmail)
        => await _userManager.SetEmailAsync(user, newEmail);
}