using Infrastructure.Persistence.Context;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Dynamic;
using System.Threading;

namespace Infrastructure.Persistence.Repositories;

public class Repository<TEntity, KeyType> : IRepository<TEntity, KeyType> where TEntity : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    //public DbSet<TEntity> Get_dbSet()
    //{
    //    return _dbSet;
    //}

    public IQueryable<TEntity> Query() => _dbSet.AsNoTracking().AsQueryable();

    public async ValueTask<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Set<TEntity>().FindAsync(id, cancellationToken);

    public async ValueTask<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    public async ValueTask<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity;
    }

    public async ValueTask UpdateExecuteAsync(Expression<Func<SetPropertyCalls<User>, SetPropertyCalls<User>>> props, CancellationToken cancellationToken = default)
    {

    }

    public async ValueTask<TEntity?> UpdatePatchAsync(int id, JsonPatchDocument<TEntity> patchDocument, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id, cancellationToken);

        if (entity == null)
        {
            throw new Exception();
        }
        patchDocument.ApplyTo(entity);

        _context.Entry(entity).State = EntityState.Modified;
        var result = await _context.SaveChangesAsync();

        return result > 0 ? entity : null;
    }

    private Dictionary<string, object?> GetUpdatingProperties(dynamic dynamicObject)
    {

        return new Dictionary<string, object?>();
        //{
        //    { FieldName1, FieldValue2 },
        //    { FieldName2, FieldValue2 }
        //}
    }

    public async ValueTask DeleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public void Delete(params TEntity[] entities)
    {
        _dbSet.RemoveRange(entities);
        _context.SaveChanges();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity?> GetByIdAsync(KeyType id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }




    Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> CombineSetters(IEnumerable<Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>>> setters)
    {
        Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> expr = sett => sett;

        foreach (var expr2 in setters)
        {
            var call = (MethodCallExpression)expr2.Body;
            expr = Expression.Lambda<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>>(
                Expression.Call(expr.Body, call.Method, call.Arguments),
                expr2.Parameters
            );
        }

        return expr;
    }
}
