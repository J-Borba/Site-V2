using projects_api.Data.Models.Enums;

namespace projects_api.Data.Dtos.Financeiro;

public record CreateAtivoDto(string Codigo,
                             AtivoTipo Tipo,
                             string Setor,
                             string Corretora,
                             decimal Quantidade,
                             decimal PrecoMedio,
                             decimal? UltimoPreco);

public record UpdateAtivoDto(string? Codigo,
                             AtivoTipo? Tipo,
                             string? Setor,
                             string? Corretora,
                             decimal? Quantidade,
                             decimal? PrecoMedio,
                             decimal? UltimoPreco);

public record ReadAtivoDto(Guid Id,
                           string Codigo,
                           AtivoTipo Tipo,
                           string Setor,
                           string Corretora,
                           decimal Quantidade,
                           decimal PrecoMedio,
                           decimal? UltimoPreco);
