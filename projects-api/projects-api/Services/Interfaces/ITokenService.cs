using projects_api.Data.Models;

namespace projects_api.Services.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IList<string> roles);
    (string RawToken, RefreshToken Entity) GenerateRefreshToken(string userId);
    string HashToken(string rawToken);
}
