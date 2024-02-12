using api.Data.Dtos.User;
using api.Models;
using api.Services.Interfaces;
using api.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<List<UserDto>> GetUserAsync()
    {
        var result = new List<UserDto>();
        var userList = _userManager.Users.ToList();
        if (userList != null && userList.Any())

            foreach (var item in userList)
            {
                result.Add(new UserDto()
                {
                    NormalizedUserName = item.UserName,
                    UserName = item.UserName,
                });
            }

        return result;
    }

    public async Task<ValidationResult> CreateUserAsync(CreateUserDto dto)
    {
        var validation = new ValidationResult();

        var user = _mapper.Map<User>(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            validation.AddErrors(result.Errors.Select(x => x.Description));
        }

        return validation;
    }


    public async Task<(ValidationResult, string)> LoginEmailUserAsync(LoginUserDto dto)
    {
        var validation = new ValidationResult();
        var token = string.Empty;

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (user == null)
        {
            validation.AddError("There's no account using these credentials.");
        }
        else
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, dto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                token = _tokenService.GetToken(user);
            }
            else
            {
                validation.AddError("An error ocurred on login.");
            }
        }

        return (validation, token);
    }

    public async Task<(ValidationResult, string)> LoginUserAsync(LoginUserDto dto)
    {
        var validation = new ValidationResult();

        var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, isPersistent: false, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            validation.AddError("An error ocurred on login.");
            return (validation, string.Empty);
        }

        var user = _signInManager.UserManager.Users.FirstOrDefault(x => x.NormalizedUserName == dto.UserName.ToUpper());
        var token = _tokenService.GetToken(user);

        return (validation, token);
    }
}