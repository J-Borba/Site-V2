using api.Services;
using api.Services.Interfaces;

namespace api.Configurations;

public static class DIConfigs
{
    public static IServiceCollection RegisterDI(this IServiceCollection services)
    {

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}
