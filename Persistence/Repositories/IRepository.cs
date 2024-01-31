﻿using System.Threading;

namespace Persistence.Repositories;

public interface IRepository<TEntity, KeyType>
    where TEntity : class
{
    IQueryable<TEntity> Query();
    ValueTask<PaginationResult<TResponse>> PaginationQuery<TResponse>(PaginationQuery paginationQuery, Expression<Func<TEntity, bool>>? predicate, Expression<Func<TEntity, TResponse>> selector, CancellationToken cancellationToken);
    ValueTask<TEntity?> GetByIdAsync(KeyType id, CancellationToken cancellationToken);
    IEnumerable<TEntity> GetAll();
    ValueTask<bool> AddAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    ValueTask UpdateExecuteAsync(Expression<Func<SetPropertyCalls<User>, SetPropertyCalls<User>>> props, CancellationToken cancellationToken = default);
    ValueTask<TEntity?> UpdatePatchAsync(int id, JsonPatchDocument<TEntity> patchDocument, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(params TEntity[] entities);
}
