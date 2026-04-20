using projects_api.Data.Dtos.User;
using projects_api.Services.Interfaces;
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
             .Produces(StatusCodes.Status401Unauthorized)
             .Produces(StatusCodes.Status403Forbidden)
             .Produces(StatusCodes.Status200OK);

        group.MapPost("/register", Register)
            .RequireRateLimiting("login")
            .Produces(StatusCodes.Status200OK)
            .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status429TooManyRequests);

        group.MapPost("/login", Login)
             .RequireRateLimiting("login")
             .Produces(StatusCodes.Status200OK)
             .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest)
             .Produces(StatusCodes.Status429TooManyRequests);

        group.MapPost("/refresh", Refresh)
             .Produces(StatusCodes.Status200OK)
             .Produces<IEnumerable<string>>(StatusCodes.Status401Unauthorized);

        group.MapPost("/logout", Logout)
             .Produces(StatusCodes.Status200OK);

        group.MapGet("/me", GetCurrentUser)
             .RequireAuthorization("Bearer")
             .Produces(StatusCodes.Status200OK)
             .Produces(StatusCodes.Status401Unauthorized);

        group.MapPatch("/me", UpdateCurrentUser)
             .RequireAuthorization("Bearer")
             .Produces(StatusCodes.Status204NoContent)
             .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest);

        return app;
    }

    private static async Task<IResult> GetUsers(IUserService userService)
    {
        var users = await userService.GetUsersAsync();
        return Results.Ok(users);
    }

    private static async Task<IResult> Register(CreateUserDto dto, IUserService userService, HttpContext context)
    {
        var createResult = await userService.CreateUserAsync(dto);
        if (!createResult.IsValid)
            return Results.BadRequest(createResult.ErrorMessages);

        var loginResult = await userService.LoginAsync(new LoginUserDto(dto.Email, dto.Password));
        if (!loginResult.Validation.IsValid)
            return Results.BadRequest(loginResult.Validation.ErrorMessages);

        SetAuthCookies(context, loginResult.AccessToken, loginResult.RefreshToken);
        return Results.Ok(new { loginResult.UserName, loginResult.Email, loginResult.Roles });
    }

    private static async Task<IResult> Login(LoginUserDto dto, IUserService userService, HttpContext context)
    {
        var result = await userService.LoginAsync(dto);
        if (!result.Validation.IsValid)
            return Results.BadRequest(result.Validation.ErrorMessages);

        SetAuthCookies(context, result.AccessToken, result.RefreshToken);

        return Results.Ok(new { result.UserName, result.Email, result.Roles });
    }

    private static async Task<IResult> Refresh(HttpContext context, IUserService userService)
    {
        var rawToken = context.Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(rawToken))
            return Results.Unauthorized();

        var result = await userService.RefreshAsync(rawToken);
        if (!result.Validation.IsValid)
            return Results.Unauthorized();

        SetAuthCookies(context, result.AccessToken, result.RefreshToken);

        return Results.Ok(new { result.UserName, result.Email, result.Roles });
    }

    private static async Task<IResult> Logout(HttpContext context, IUserService userService)
    {
        var rawToken = context.Request.Cookies["refresh_token"];
        if (!string.IsNullOrEmpty(rawToken))
            await userService.LogoutAsync(rawToken);

        context.Response.Cookies.Delete("access_token");
        context.Response.Cookies.Delete("refresh_token", new CookieOptions { Path = "/user" });
        return Results.Ok();
    }

    private static IResult GetCurrentUser(ClaimsPrincipal user) =>
        Results.Ok(new
        {
            userName = user.FindFirstValue(ClaimTypes.Name),
            email = user.FindFirstValue(ClaimTypes.Email),
            roles = user.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList()
        });

    private static async Task<IResult> UpdateCurrentUser(UpdateUserDto dto, IUserService userService, ClaimsPrincipal user)
    {
        var currentUserEmail = user.FindFirstValue(ClaimTypes.Email)!;
        var result = await userService.UpdateCurrentUserAsync(dto, currentUserEmail);
        return result.IsValid ? Results.NoContent() : Results.BadRequest(result.ErrorMessages);
    }

    private static void SetAuthCookies(HttpContext context, string accessToken, string refreshToken)
    {
        var now = DateTimeOffset.UtcNow;
        context.Response.Cookies.Append("access_token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = now.AddMinutes(15)
        });
        context.Response.Cookies.Append("refresh_token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Path = "/user",
            Expires = now.AddDays(7)
        });
    }
}
