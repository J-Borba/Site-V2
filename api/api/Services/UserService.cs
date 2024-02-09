using api.Data.Dtos.User;
using api.Models;
using api.Services.Interfaces;
using api.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

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