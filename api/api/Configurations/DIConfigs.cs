using api.Services;
using api.Services.Interfaces;

namespace api.Configurations;

public static class DIConfigs
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        _ = services.AddScoped<IUserService, UserService>();
        _ = services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
