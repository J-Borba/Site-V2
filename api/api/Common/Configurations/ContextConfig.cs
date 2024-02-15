using api.Common.Settings;
using api.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace api.Common.Configurations;
public static class ContextConfig
{
    public static IServiceCollection AddContextDependecies(this IServiceCollection services, IConfiguration configuration, out AppSettings appSettings)
    {
        var cnf = new AppSettings();
        services.EnvironmentValues(configuration, out cnf);

        _ = services.AddDbContext<SiteDbContext>(config =>
        {
            _ = config.UseMySql(cnf.ConnectionStrings.DefaultConnection, ServerVersion.AutoDetect(cnf.ConnectionStrings.DefaultConnection));
        });

        appSettings = cnf;

        return services;
    }

    private static void EnvironmentValues(this IServiceCollection services, IConfiguration configuration, out AppSettings appSettings)
    {
        var cnf = new AppSettings();

        cnf.ConnectionStrings.DefaultConnection = EValues("APP_CONNECTIONSTRING", "DefaultConnection", configuration, true);

        cnf.Secrets.SymmetricSecurityKey = EValues("APP_SYMMETRICSECURITYKEY", "Secrets:SymmetricSecurityKey", configuration);

        appSettings = cnf;

        _ = services.AddSingleton(cnf);
    }
    private static string EValues(string envName, string appName, IConfiguration configuration, bool isCnStr = false)
    {
        return isCnStr
            ? (Environment.GetEnvironmentVariable(envName) ?? configuration.GetConnectionString(appName)) ?? string.Empty
            : (Environment.GetEnvironmentVariable(envName) ?? configuration.GetSection(appName)?.Value) ?? string.Empty;
    }
}