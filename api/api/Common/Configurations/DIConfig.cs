using api.Data.Repositories;
using api.Data.Repositories.Interfaces;
using api.Services;
using api.Services.Interfaces;

namespace api.Common.Configurations;

public static class DIConfig
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        #region Services
        _ = services.AddScoped<IUserService, UserService>();
        _ = services.AddScoped<ITokenService, TokenService>();
        #endregion

        #region Repositories
        _ = services.AddScoped<IUserRepository, UserRepository>();
        #endregion

        return services;
    }
}
