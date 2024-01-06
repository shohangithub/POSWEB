namespace POSWEB.Server.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Query();
        ValueTask<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(params T[] entities);
    }
}
