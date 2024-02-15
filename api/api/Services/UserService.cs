using api.Data.Dtos.User;
using api.Models;
using api.Services.Interfaces;
using api.Validation;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace api.Services;

public class UserService : IUserService
{
    #region Dependency Injections
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
    #endregion

    public (ValidationResult, IEnumerable<ReadUserDto>) GetUsersAsync(IEnumerable<Claim> currentUserClaims)
    {
        var result = new List<ReadUserDto>();
        var validation = new ValidationResult();

        var currentUserName = currentUserClaims.First(c => c.Type == "username").Value;

        if (string.Equals(currentUserName, "Borba")) //Change to IsSupervisor Claim
        {
            result = _mapper.Map<List<ReadUserDto>>(_userManager.Users);
        }
        else
        {
            validation.AddError("Access Denied.");
        }

        return (validation, result);
    }

    public async Task<ValidationResult> CreateUserAsync(CreateUserDto dto)
    {
        var validation = new ValidationResult();

        dto.UserName = string.IsNullOrEmpty(dto.UserName) ? dto.Email : dto.UserName;

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
        var token = string.Empty;

        var user = await _userManager.FindByEmailAsync(dto.Email);

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
                validation.AddError("There's no account using these credentials.");
            }
        }

        return (validation, token);
    }

    public async Task<ValidationResult> UpdateUserAsync(UpdateUserDto dto)
    {
        var validation = new ValidationResult();
        var user = await _userManager.FindByEmailAsync(dto.CurrentEmail);

        if (user == null)
        {
            validation.AddError("User Not Found");
        }
        //else
        //{
        //    if (!string.Equals(dto.NewUserName, user.UserName))
        //    {
        //        _ = _mapper.Map(dto.NewUserName, user.UserName);
        //    }
        //    if (!string.Equals(dto.NewEmail, user.Email))
        //    {
        //        _ = _mapper.Map(dto.NewEmail, user.Email);
        //    }
        //}

        return validation;
    }

    public IEnumerable<ReadUserDto> GetUsersAsync(ClaimsPrincipal currentUser)
    {
        throw new NotImplementedException();
    }
}
