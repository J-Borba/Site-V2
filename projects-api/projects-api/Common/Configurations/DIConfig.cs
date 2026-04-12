using projects_api.Data.Repositories;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services;
using projects_api.Services.Interfaces;

namespace projects_api.Common.Configurations;

public static class DIConfig
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
