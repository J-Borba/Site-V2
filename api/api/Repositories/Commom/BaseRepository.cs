using api.Data;

namespace api.Repositories.Commom;

public class BaseRepository
{
    protected readonly SiteDbContext _context;

    public BaseRepository(SiteDbContext context)
    {
        _context = context;
    }
}
