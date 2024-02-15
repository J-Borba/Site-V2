using api.Data.Dtos.User;
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

    [HttpGet, Authorize("Bearer")]
    public IActionResult GetUsers()
    {
        var currentUserClaims = HttpContext.User.Claims;
        (var validation, var users) = _userService.GetUsersAsync(currentUserClaims);

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

    [HttpPut, Authorize("Bearer")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
    {
        var result = await _userService.UpdateUserAsync(dto);

        return result.IsValid ? Ok("User updated successfully.") : BadRequest(result.ErrorMessages);
    }
}