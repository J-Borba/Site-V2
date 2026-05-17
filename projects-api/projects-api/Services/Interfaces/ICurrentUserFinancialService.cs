using projects_api.Common.Validation;

namespace projects_api.Services.Interfaces;

public interface ICurrentUserFinancialService<TRead, TCreate, TUpdate>
{
    Task<List<TRead>> ListAsync();
    Task<(TRead? dto, ValidationResult result)> CreateAsync(TCreate input);
    Task<(TRead? dto, ValidationResult result)> UpdateAsync(Guid id, TUpdate patch);
    Task<ValidationResult> DeleteAsync(Guid id);
}
