

using Application.Framework;

namespace Application.Contractors;

public interface IProductCategoryService
{
    ValueTask<IEnumerable<ProductCategoryListResponse>> ListAsync(CancellationToken cancellationToken = default);
    ValueTask<PaginationResult<ProductCategoryListResponse>> PaginationListAsync(PaginationQuery requestQuery, CancellationToken cancellationToken = default);
    ValueTask<ProductCategoryResponse> GetByIdAsync(short id, CancellationToken cancellationToken = default);
    ValueTask<ProductCategoryResponse> AddAsync(ProductCategoryRequest user, CancellationToken cancellationToken = default);
    ValueTask<ProductCategoryResponse> UpdateAsync(short id, ProductCategoryRequest user, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(short id, CancellationToken cancellationToken = default);
    ValueTask<bool> IsExistsAsync(short id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Lookup<short>>> GetLookup(Expression<Func<ProductCategory, bool>> predicate, CancellationToken cancellationToken = default);
}
