namespace projects_api.Endpoints;

public static class EndpointMapper
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUserEndpoints();
        app.MapFinanceiroEndpoints();

        return app;
    }
}
