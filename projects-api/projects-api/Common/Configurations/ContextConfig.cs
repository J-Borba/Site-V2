using projects_api.Common.Settings;
using projects_api.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace projects_api.Common.Configurations;

public static class ContextConfig
{
    public static IServiceCollection AddContextDependencies(this IServiceCollection services,
                                                            IConfiguration configuration,
                                                            out AppSettings appSettings)
    {
        appSettings = new AppSettings();
        configuration.GetSection(nameof(AppSettings)).Bind(appSettings);
        services.AddSingleton(appSettings);

        services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
