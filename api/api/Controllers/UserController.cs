using api.Data.Dtos.User;
using api.Services.Enumerators;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController, Route("[controller]")]
public class UserController : ControllerBase
{
    #region Dependency Injections
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    #endregion

    [HttpGet]
    [Authorize("Bearer")]
    public async Task<IActionResult> GetUsers()
    {
        var currentUserName = HttpContext.User.Claims.First(c => c.Type == nameof(UserPropsEnum.UserName)).Value;
        (var validation, var users) = await _userService.GetUsersAsync(currentUserName);

        return validation.IsValid ? Ok(users) : BadRequest(validation.ErrorMessages);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _userService.CreateUserAsync(dto);

        return result.IsValid ? Ok("User created successfully.") : BadRequest(result.ErrorMessages);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        (var validation, var token) = await _userService.LoginUserAsync(dto);

        return validation.IsValid ? Ok(token) : BadRequest(validation.ErrorMessages);
    }

    [HttpPost("Update/Current")]
    [Authorize("Bearer")]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto dto)
    {
        var currentUserEmail = HttpContext.User.Claims.First(x => x.Type == "Email").Value;
        var result = await _userService.UpdateCurrentUserAsync(dto, currentUserEmail);

        return result.IsValid ? Ok("User updated successfully.") : BadRequest(result.ErrorMessages);
    }
}