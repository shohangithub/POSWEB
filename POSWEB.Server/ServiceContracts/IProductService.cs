using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using System.Linq.Expressions;

namespace POSWEB.Server.ServiceContracts
{
    public interface IProductService
    {
        ValueTask<List<Product>> ProductListAsync();
        ValueTask<Product> GetByIdAsync(uint id, CancellationToken cancellationToken = default);
        ValueTask<bool> AddAsync(Product product);
        ValueTask<bool> UpdateAsync(uint id, Product product);
        ValueTask<bool> isExistsAsync(uint id);
        ValueTask<IEnumerable<LookupRecordStruct>> ProductsLookup(Expression<Func<Product, bool>> predicate);
    }
}
