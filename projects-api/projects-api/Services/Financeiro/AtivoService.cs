using AutoMapper;
using projects_api.Common.Validation;
using projects_api.Data.Dtos.Financeiro;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services.Interfaces;

namespace projects_api.Services.Financeiro;

public class AtivoService(ICurrentUserFinancialRepository<Ativo> repository,
                          ICurrentUserService currentUserService,
                          IMapper mapper) : CurrentUserFinancialService<Ativo, ReadAtivoDto, CreateAtivoDto, UpdateAtivoDto>(repository, currentUserService, mapper)
{
    protected override ValidationResult ValidateCreate(CreateAtivoDto input)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(input.Codigo))
            result.AddError("Código é obrigatório.");

        if (input.Quantidade <= 0)
            result.AddError("Quantidade deve ser maior que zero.");

        if (input.PrecoMedio < 0)
            result.AddError("Preço médio não pode ser negativo.");

        if (input.UltimoPreco.HasValue && input.UltimoPreco < 0)
            result.AddError("Último preço não pode ser negativo.");

        return result;
    }

    protected override ValidationResult ValidateUpdate(UpdateAtivoDto patch)
    {
        var result = new ValidationResult();
        if (patch.Codigo is not null && string.IsNullOrWhiteSpace(patch.Codigo))
        {
            result.AddError("Código não pode ser vazio.");
        }
        if (patch.Quantidade.HasValue && patch.Quantidade <= 0)
        {
            result.AddError("Quantidade deve ser maior que zero.");
        }
        if (patch.PrecoMedio.HasValue && patch.PrecoMedio < 0)
        {
            result.AddError("Preço médio não pode ser negativo.");
        }
        if (patch.UltimoPreco.HasValue && patch.UltimoPreco < 0)
        {
            result.AddError("Último preço não pode ser negativo.");
        }
        return result;
    }

    protected override void ApplyPatch(Ativo entity, UpdateAtivoDto patch)
    {
        if (patch.Codigo is not null) entity.Codigo = patch.Codigo;
        if (patch.Tipo.HasValue) entity.Tipo = patch.Tipo.Value;
        if (patch.Setor is not null) entity.Setor = patch.Setor;
        if (patch.Corretora is not null) entity.Corretora = patch.Corretora;
        if (patch.Quantidade.HasValue) entity.Quantidade = patch.Quantidade.Value;
        if (patch.PrecoMedio.HasValue) entity.PrecoMedio = patch.PrecoMedio.Value;
        if (patch.UltimoPreco.HasValue) entity.UltimoPreco = patch.UltimoPreco.Value;
    }
}
