using projects_api.Data.Abstractions;

namespace projects_api.Data.Repositories.Interfaces;

public interface ICurrentUserFinancialRepository<T> where T : class, ICurrentUserFinancialEntity
{
    Task<List<T>> ListByUserAsync(string userId);
    Task<T?> GetByIdAsync(Guid id, string userId);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
