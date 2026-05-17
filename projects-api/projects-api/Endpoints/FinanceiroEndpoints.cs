using projects_api.Data.Dtos.Financeiro;
using projects_api.Services.Financeiro;
using projects_api.Services.Interfaces;

namespace projects_api.Endpoints;

public static class FinanceiroEndpoints
{
    public static IEndpointRouteBuilder MapFinanceiroEndpoints(this IEndpointRouteBuilder app)
    {
        MapEndPoint<AtivoService, ReadAtivoDto, CreateAtivoDto, UpdateAtivoDto>(app, "/financeiro/ativos", "Financeiro/Ativos");
        MapEndPoint<ProventoService, ReadProventoDto, CreateProventoDto, UpdateProventoDto>(app, "/financeiro/proventos", "Financeiro/Proventos");
        return app;
    }

    private static void MapEndPoint<TService, TRead, TCreate, TUpdate>(IEndpointRouteBuilder app,
                                                                       string path,
                                                                       string tag)
        where TService : class, ICurrentUserFinancialService<TRead, TCreate, TUpdate>
    {
        var group = app.MapGroup(path)
                       .WithTags(tag)
                       .RequireAuthorization("Bearer");

        group.MapGet("", async (TService service) => Results.Ok(await service.ListAsync()))
             .Produces<IEnumerable<TRead>>();

        group.MapPost("", async (TCreate input, TService service) =>
        {
            var (dto, result) = await service.CreateAsync(input);
            return result.IsValid ? Results.Ok(dto) : Results.BadRequest(result.ErrorMessages);
        })
        .Produces<TRead>()
        .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest);

        group.MapPut("{id:guid}", async (Guid id, TUpdate patch, TService service) =>
        {
            var (dto, result) = await service.UpdateAsync(id, patch);
            return result.IsValid ? Results.Ok(dto) : Results.BadRequest(result.ErrorMessages);
        })
        .Produces<TRead>()
        .Produces<IEnumerable<string>>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("{id:guid}", async (Guid id, TService service) =>
        {
            var result = await service.DeleteAsync(id);
            return result.IsValid ? Results.NoContent() : Results.NotFound(result.ErrorMessages);
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
