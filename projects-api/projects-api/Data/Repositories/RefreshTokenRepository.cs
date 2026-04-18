using projects_api.Data.Contexts;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace projects_api.Data.Repositories;

public class RefreshTokenRepository(ApiDbContext context) : IRefreshTokenRepository
{
    public async Task CreateAsync(RefreshToken token)
    {
        context.DbRefreshTokens.Add(token);
        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByHashAsync(string tokenHash) =>
        await context.DbRefreshTokens
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);

    public async Task RevokeAsync(RefreshToken token)
    {
        token.IsRevoked = true;
        await context.SaveChangesAsync();
    }

    public async Task RevokeAllForUserAsync(string userId) =>
        await context.DbRefreshTokens
            .Where(t => t.UserId == userId && !t.IsRevoked)
            .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsRevoked, true));
}
