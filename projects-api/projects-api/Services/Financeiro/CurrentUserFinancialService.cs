using AutoMapper;
using projects_api.Data.Abstractions;
using projects_api.Common.Validation;
using projects_api.Data.Repositories.Interfaces;
using projects_api.Services.Interfaces;

namespace projects_api.Services.Financeiro;

public abstract class CurrentUserFinancialService<TEntity, TRead, TCreate, TUpdate>(ICurrentUserFinancialRepository<TEntity> repo,
                                                                                    ICurrentUserService currentUser,
                                                                                    IMapper mapper) : ICurrentUserFinancialService<TRead, TCreate, TUpdate> where TEntity : class, ICurrentUserFinancialEntity
{
    protected abstract ValidationResult ValidateCreate(TCreate input);
    protected abstract ValidationResult ValidateUpdate(TUpdate patch);
    protected abstract void ApplyPatch(TEntity entity, TUpdate patch);

    public async Task<List<TRead>> ListAsync()
    {
        return mapper.Map<List<TRead>>(await repo.ListByUserAsync(currentUser.Id));
    }

    public async Task<(TRead? dto, ValidationResult result)> CreateAsync(TCreate input)
    {
        var result = ValidateCreate(input);
        if (!result.IsValid)
        {
            return (default, result);
        }

        var entity = mapper.Map<TEntity>(input);
        entity.UserId = currentUser.Id;

        await repo.AddAsync(entity);
        await repo.SaveChangesAsync();
        return (mapper.Map<TRead>(entity), result);
    }

    public async Task<(TRead? dto, ValidationResult result)> UpdateAsync(Guid id, TUpdate patch)
    {
        var result = ValidateUpdate(patch);
        if (!result.IsValid)
            return (default, result);

        var entity = await TryFindAsync(id, result);
        if (!result.IsValid)
            return (default, result);

        ApplyPatch(entity!, patch);
        repo.Update(entity!);

        await repo.SaveChangesAsync();
        return (mapper.Map<TRead>(entity), result);
    }

    public async Task<ValidationResult> DeleteAsync(Guid id)
    {
        var result = new ValidationResult();

        var entity = await TryFindAsync(id, result);
        if (!result.IsValid)
            return result;

        repo.Delete(entity!);

        await repo.SaveChangesAsync();
        return result;
    }

    private async Task<TEntity?> TryFindAsync(Guid id, ValidationResult validation)
    {
        var entity = await repo.GetByIdAsync(id, currentUser.Id);

        if (entity is null)
            validation.AddError("Registro não encontrado.");

        return entity;
    }
}
