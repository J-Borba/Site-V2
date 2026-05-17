using projects_api.Data.Models.Enums;

namespace projects_api.Data.Dtos.Financeiro;

public record CreateProventoDto(DateOnly Data,
                                string Ticker,
                                ProventoTipo Tipo,
                                decimal ValorLiquido);

public record UpdateProventoDto(DateOnly? Data,
                                string? Ticker,
                                ProventoTipo? Tipo,
                                decimal? ValorLiquido);

public record ReadProventoDto(Guid Id,
                              DateOnly Data,
                              string Ticker,
                              ProventoTipo Tipo,
                              decimal ValorLiquido);
