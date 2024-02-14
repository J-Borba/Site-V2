using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class SiteDbContext : IdentityDbContext<User>
{
    public SiteDbContext(DbContextOptions options) : base(options)
    {

        Database.Migrate();
    }
}
