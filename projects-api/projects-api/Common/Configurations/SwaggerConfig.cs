using Scalar.AspNetCore;

namespace projects_api.Common.Configurations;

public static class SwaggerConfig
{
    public static IServiceCollection AddOpenApiConfigs(this IServiceCollection services)
    {
        services.AddOpenApi();
        return services;
    }

    public static WebApplication UseOpenApiConfigs(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        return app;
    }
}
