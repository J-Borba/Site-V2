using projects_api.Common.Settings;
using projects_api.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace projects_api.Data.Seeders;

public static class SuperAdminSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var appSettings = services.GetRequiredService<AppSettings>();

        const string role = "SuperAdmin";

        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));

        var email = appSettings.Secrets.SuperAdminEmail;
        if (string.IsNullOrEmpty(email) || await userManager.FindByEmailAsync(email) is not null)
            return;

        var user = new User { UserName = email, Email = email };
        var result = await userManager.CreateAsync(user, appSettings.Secrets.SuperAdminPassword);

        if (result.Succeeded)
            await userManager.AddToRoleAsync(user, role);
    }
}
