namespace Infrastructure.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    IQueryable<T> Query();
    ValueTask<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    IEnumerable<T> GetAll();
    ValueTask AddAsync(T entity, CancellationToken cancellationToken);
    ValueTask UpdateAsync(T entity, CancellationToken cancellationToken);
    ValueTask DeleteAsync(T entity);
    void Delete(params T[] entities);
}
