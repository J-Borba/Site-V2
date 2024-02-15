using api.Common.Validation;
using api.Data.Dtos.User;
using api.Data.Models;
using api.Data.Repositories.Interfaces;
using api.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace api.Services;

public class UserService : IUserService
{
    #region Dependency Injections
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _repository;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, ITokenService tokenService, IUserRepository userRepository)
    {
        _mapper = mapper;
        _tokenService = tokenService;
        _repository = userRepository;
    }
    #endregion

    public async Task<(ValidationResult, IEnumerable<ReadUserDto>)> GetUsersAsync(string currentUserName)
    {
        var result = new List<ReadUserDto>();
        var validation = new ValidationResult();

        if (string.Equals(currentUserName, "Borba")) //Change to IsSupervisor Claim
        {
            result = _mapper.Map<List<ReadUserDto>>(await _repository.GetAllAsync());
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

        var result = await _repository.CreateUserAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            validation.AddErrors(result.Errors.Select(x => x.Description));
        }

        return validation;
    }

    public async Task<(ValidationResult, string)> LoginUserAsync(LoginUserDto dto)
    {
        var errorMsg = "There's no account using these credentials.";
        var validation = new ValidationResult();
        var token = string.Empty;

        var user = await _repository.GetUserByEmailAsync(dto.Email);

        if (user == null)
        {
            validation.AddError(errorMsg);
        }
        else
        {
            var result = await _repository.LoginUserAsync(user.UserName, dto.Password);

            if (result.Succeeded)
            {
                token = _tokenService.GetToken(user);
            }
            else
            {
                validation.AddError(errorMsg);
            }
        }

        return (validation, token);
    }

    public async Task<ValidationResult> UpdateCurrentUserAsync(UpdateUserDto dto, string currentUserEmail)
    {
        var validation = new ValidationResult();
        var user = await _repository.GetUserByEmailAsync(currentUserEmail);
        var commit = false;

        if (string.IsNullOrEmpty(dto.NewUserName))
            dto.NewUserName = user.UserName;

        if (string.IsNullOrEmpty(dto.NewEmail))
            dto.NewEmail = user.Email;

        var repositoryValidation = new IdentityResult();
        if (!string.Equals(user.UserName, dto.NewUserName))
        {
            commit = true;
            repositoryValidation = await _repository.UpdateUserNameAsync(user, dto.NewUserName);

            if (!repositoryValidation.Succeeded)
            {
                validation.AddErrors(repositoryValidation.Errors.Select(x => x.Description));
            }
        }
        if (!string.Equals(user.Email, dto.NewEmail))
        {
            commit = true;
            repositoryValidation = await _repository.UpdateUserEmailAsync(user, dto.NewEmail);

            if (!repositoryValidation.Succeeded)
            {
                validation.AddErrors(repositoryValidation.Errors.Select(x => x.Description));
            }
        }

        if (!commit)
            validation.AddError("No changes were made.");

        return validation;
    }
}
