using AutoMapper;
using projects_api.Common.Validation;
using projects_api.Data.Dtos.Financeiro;
using projects_api.Data.Models;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services.Interfaces;

namespace projects_api.Services.Financeiro;

public class ProventoService(ICurrentUserFinancialRepository<Provento> repository,
                             ICurrentUserService currentUserService,
                             IMapper mapper) : CurrentUserFinancialService<Provento, ReadProventoDto, CreateProventoDto, UpdateProventoDto>(repository, currentUserService, mapper)
{
    protected override ValidationResult ValidateCreate(CreateProventoDto input)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(input.Ticker))
            result.AddError("Ticker é obrigatório.");

        if (input.ValorLiquido <= 0)
            result.AddError("Valor líquido deve ser maior que zero.");

        return result;
    }

    protected override ValidationResult ValidateUpdate(UpdateProventoDto patch)
    {
        var result = new ValidationResult();

        if (patch.Ticker is not null && string.IsNullOrWhiteSpace(patch.Ticker))
            result.AddError("Ticker não pode ser vazio.");

        if (patch.ValorLiquido.HasValue && patch.ValorLiquido <= 0)
            result.AddError("Valor líquido deve ser maior que zero.");

        return result;
    }

    protected override void ApplyPatch(Provento entity, UpdateProventoDto patch)
    {
        if (patch.Data.HasValue)
            entity.Data = patch.Data.Value;

        if (patch.Ticker is not null)
            entity.Ticker = patch.Ticker;

        if (patch.Tipo.HasValue)
            entity.Tipo = patch.Tipo.Value;

        if (patch.ValorLiquido.HasValue)
            entity.ValorLiquido = patch.ValorLiquido.Value;
    }
}
