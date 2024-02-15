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

    /// <summary>
    /// Get all users in the database (if you have permission)
    /// </summary>
    /// <returns>
    /// IEnumerable
    /// </returns>
    ///  <response code="200">If search is successfull</response>
    ///  <response code="404">If search has no results</response>
    ///  <response code="400">If search realized with errors</response>
    [HttpGet]
    [Authorize("Bearer")]
    public async Task<IActionResult> GetUsers()
    {
        var currentUserName = HttpContext.User.Claims.First(c => c.Type == nameof(UserPropsEnum.UserName)).Value;
        (var validation, var users) = await _userService.GetUsersAsync(currentUserName);

        return validation.IsValid ? (users.Any() ? Ok(users) : NotFound()) : BadRequest(validation.ErrorMessages);
    }

    /// <summary>
    /// Create user in database
    /// </summary>
    /// <param name="dto">User Dto to create (if username not informed, it becomes the same as email)</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">If created successfully</response>
    /// <response code="400">If returned errors</response>
    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDto dto)
    {
        var result = await _userService.CreateUserAsync(dto);

        return result.IsValid ? Ok("User created successfully.") : BadRequest(result.ErrorMessages);
    }

    /// <summary>
    /// Login with Email and Password
    /// </summary>
    /// <param name="dto">User Dto to Login</param>
    /// <returns>
    /// Token
    /// </returns>
    /// <response code="200">If logged successfully</response>
    /// <response code="400">If request failed</response>
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        (var validation, var token) = await _userService.LoginUserAsync(dto);

        return validation.IsValid ? Ok(token) : BadRequest(validation.ErrorMessages);
    }

    /// <summary>
    /// Update current logged user
    /// </summary>
    /// <param name="dto">User Dto to update (if field is blank, it does not change)</param>
    /// <returns>
    /// IActionResult
    /// </returns>
    /// <response code="200">If updated successfully</response>
    /// <response code="400">If returned errors</response>
    [HttpPost("Update/Current")]
    [Authorize("Bearer")]
    public async Task<IActionResult> UpdateCurrentUser([FromBody] UpdateUserDto dto)
    {
        var currentUserEmail = HttpContext.User.Claims.First(c => c.Type == nameof(UserPropsEnum.Email)).Value;
        var result = await _userService.UpdateCurrentUserAsync(dto, currentUserEmail);

        return result.IsValid ? Ok("User updated successfully.") : BadRequest(result.ErrorMessages);
    }
}