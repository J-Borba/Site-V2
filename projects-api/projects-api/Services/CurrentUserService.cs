using projects_api.Services.Interfaces;
using System.Security.Claims;

namespace projects_api.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public string Id => GetCurrentUser()?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    public string Email => GetCurrentUser()?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    public string UserName => GetCurrentUser()?.FindFirstValue(ClaimTypes.Name) ?? string.Empty;
    public IEnumerable<string> Roles => GetCurrentUser()?.FindAll(ClaimTypes.Role).Select(c => c.Value) ?? [];

    private ClaimsPrincipal? GetCurrentUser()
    {
        return httpContextAccessor.HttpContext?.User;
    }
}
