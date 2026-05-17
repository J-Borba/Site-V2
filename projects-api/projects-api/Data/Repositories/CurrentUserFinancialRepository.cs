using Microsoft.EntityFrameworkCore;
using projects_api.Data.Abstractions;
using projects_api.Data.Contexts;
using projects_api.Data.Repositories.Interfaces;

namespace projects_api.Data.Repositories;

public class CurrentUserFinancialRepository<T>(ApiDbContext db) : ICurrentUserFinancialRepository<T>
    where T : class, ICurrentUserFinancialEntity
{
    public Task<List<T>> ListByUserAsync(string userId) =>
        db.Set<T>().Where(e => e.UserId == userId).ToListAsync();

    public Task<T?> GetByIdAsync(Guid id, string userId) =>
        db.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

    public Task AddAsync(T entity) =>
        db.Set<T>().AddAsync(entity).AsTask();

    public void Update(T entity) =>
        db.Set<T>().Update(entity);

    public void Delete(T entity) =>
        db.Set<T>().Remove(entity);

    public Task SaveChangesAsync() =>
        db.SaveChangesAsync();
}
