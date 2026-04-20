using projects_api.Data.Models;

namespace projects_api.Data.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    Task CreateAsync(RefreshToken token);
    Task<RefreshToken?> GetByHashAsync(string tokenHash);
    Task RevokeAsync(RefreshToken token);
    Task RevokeAllForUserAsync(string userId);
}
