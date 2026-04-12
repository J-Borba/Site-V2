using projects_api.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace projects_api.Data.Contexts;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : IdentityDbContext<User>(options)
{
}
