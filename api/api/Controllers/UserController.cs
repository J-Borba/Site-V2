using api.Data.Dtos.User;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController, Route("[controller]")]
public class UserController : ControllerBase
{
    #region Depedency Injections
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    #endregion

    [HttpPost("Signin")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _userService.CreateUserAsync(dto);

        return result.IsValid ? Ok("User created successfully.") : BadRequest(result.ErrorMessages);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        (var validation, string token) = await _userService.LoginUserAsync(dto);

        return validation.IsValid ? Ok($"User authenticated.\nToken: {token}") : BadRequest(validation.ErrorMessages);
    }

    [Authorize("Bearer")]
    [HttpGet()]
    public async Task<IActionResult> GetAll()
        => Ok(await _userService.GetUserAsync());

}