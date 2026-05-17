using Microsoft.EntityFrameworkCore;
using projects_api.Data.Contexts;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;

namespace projects_api.Data.Repositories;

public class RefreshTokenRepository(ApiDbContext context) : IRefreshTokenRepository
{
    public async Task CreateAsync(RefreshToken token)
    {
        context.RefreshTokens.Add(token);

        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetByHashAsync(string tokenHash)
    {
        return await context.RefreshTokens.Include(t => t.User)
                                          .FirstOrDefaultAsync(t => t.TokenHash == tokenHash);
    }

    public async Task RevokeAsync(RefreshToken token)
    {
        token.IsRevoked = true;
        await context.SaveChangesAsync();
    }

    public async Task RevokeAllForUserAsync(string userId)
    {
        await context.RefreshTokens.Where(t => t.UserId == userId && !t.IsRevoked)
                                   .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsRevoked, true));
    }
}
