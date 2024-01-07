using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using System.Linq.Expressions;

namespace POSWEB.Server.ServiceContracts
{
    public interface IProductCategoryService
    {
        ValueTask<List<ProductCategory>> ProductCategoryListAsync();
        ValueTask<ProductCategory> GetByIdAsync(uint id, CancellationToken cancellationToken = default);
        ValueTask<bool> AddAsync(ProductCategory ProductCategory);
        ValueTask<bool> UpdateAsync(uint id, ProductCategory ProductCategory);
        ValueTask<bool> isExistsAsync(uint id);
        ValueTask<IEnumerable<LookupRecordShortStruct>> ProductCategoriesLookup(Expression<Func<ProductCategory, bool>> predicate);
    }
}
