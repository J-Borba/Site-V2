using projects_api.Data.Models;

namespace projects_api.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user, IList<string> roles);
}
