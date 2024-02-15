using api.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Contexts;

public class SiteDbContext : IdentityDbContext<User>
{
    public SiteDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }
}
