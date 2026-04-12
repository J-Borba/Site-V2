using projects_api.Common.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace projects_api.Common.Configurations;

public static class AuthConfig
{
    public static IServiceCollection AddJwt(this IServiceCollection services, AppSettings appSettings)
    {
        var key = Encoding.UTF8.GetBytes(appSettings.Secrets.SymmetricSecurityKey);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Bearer", policy => policy.RequireAuthenticatedUser());
            options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
        });

        return services;
    }
}
