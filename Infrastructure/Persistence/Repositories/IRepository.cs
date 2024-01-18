using Microsoft.AspNetCore.JsonPatch;

namespace Infrastructure.Persistence.Repositories;

public interface IRepository<TEntity,KeyType> 
    where TEntity : class
{
    IQueryable<TEntity> Query();
    ValueTask<TEntity?> GetByIdAsync(KeyType id, CancellationToken cancellationToken);
    IEnumerable<TEntity> GetAll();
    ValueTask<bool> AddAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask<TEntity?> PatchUpdate(int id, JsonPatchDocument<TEntity> patchDocument, CancellationToken cancellationToken = default);
    ValueTask DeleteAsync(TEntity entity);
    void Delete(params TEntity[] entities);
}
