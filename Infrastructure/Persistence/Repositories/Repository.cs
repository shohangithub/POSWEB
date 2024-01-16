using Infrastructure.Persistence.Context;

namespace Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DbContext context, ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _context = context;
        _dbSet = context.Set<T>();
    }

    //public DbSet<T> Get_dbSet()
    //{
    //    return _dbSet;
    //}

    public IQueryable<T> Query() => _dbSet.AsNoTracking().AsQueryable();

    public async ValueTask<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default) => await _context.Set<T>().FindAsync(id, cancellationToken);

    public async ValueTask AddAsync(T entity, CancellationToken cancellationToken = default) => await _dbSet.AddAsync(entity, cancellationToken);

    public async ValueTask UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async ValueTask DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
    public void Delete(params T[] entities)
    {
        _dbSet.RemoveRange(entities);
        _context.SaveChanges();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
