using projects_api.Data.Dtos.User;
using projects_api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace projects_api.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/user").WithTags("User");

        group.MapGet("/", GetUsers)
            .RequireAuthorization("SuperAdmin")
            .Produces<IEnumerable<ReadUserDto>>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status403Forbidden);

        group.MapPost("/register", Register)
            .Produces(StatusCodes.Status200OK)
            .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest);

        group.MapPost("/login", Login)
            .Produces<string>()
            .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest);

        group.MapPatch("/me", UpdateCurrentUser)
            .RequireAuthorization("Bearer")
            .Produces(StatusCodes.Status200OK)
            .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> GetUsers(IUserService userService)
    {
        var users = await userService.GetUsersAsync();
        return users.Any() ? Results.Ok(users) : Results.NotFound();
    }

    private static async Task<IResult> Register(CreateUserDto dto, IUserService userService)
    {
        var result = await userService.CreateUserAsync(dto);
        return result.IsValid ? Results.Ok("User created successfully.") : Results.BadRequest(result.ErrorMessages);
    }

    private static async Task<IResult> Login(LoginUserDto dto, IUserService userService)
    {
        var (validation, token) = await userService.LoginAsync(dto);
        return validation.IsValid ? Results.Ok(token) : Results.BadRequest(validation.ErrorMessages);
    }

    private static async Task<IResult> UpdateCurrentUser(UpdateUserDto dto, IUserService userService, ClaimsPrincipal user)
    {
        var currentUserEmail = user.FindFirstValue(ClaimTypes.Email)!;
        var result = await userService.UpdateCurrentUserAsync(dto, currentUserEmail);
        return result.IsValid ? Results.Ok("User updated successfully.") : Results.BadRequest(result.ErrorMessages);
    }
}
