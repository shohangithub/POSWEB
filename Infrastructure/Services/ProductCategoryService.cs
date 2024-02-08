


using Infrastructure.Services.Common;

namespace Infrastructure.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IRepository<ProductCategory, short> _repository;
    private readonly DefaultValueInjector _defaultValueInjector;
    public ProductCategoryService(IRepository<ProductCategory, short> repository, DefaultValueInjector defaultValueInjector)
    {
        _repository = repository;
        _defaultValueInjector = defaultValueInjector;
    }

    public async ValueTask<ProductCategoryResponse> AddAsync(ProductCategoryRequest user, CancellationToken cancellationToken = default)
    {
        ProductCategoryValidator validator = new(_repository);
        await validator.ValidateAndThrowAsync(user, cancellationToken);

        var entity = user.Adapt<ProductCategory>();
        _defaultValueInjector.InjectCreatingAudit<ProductCategory, short>(entity);
        var result = await _repository.AddAsync(entity, cancellationToken);
        var response = result ? entity.Adapt<ProductCategoryResponse>() : null;
        return response;
    }

    public async ValueTask<bool> DeleteAsync(short id, CancellationToken cancellationToken = default)
    {
        var existingData = await _repository.GetByIdAsync(id, cancellationToken);
        if (existingData is null) throw new ArgumentNullException(nameof(existingData));
        return await _repository.DeleteAsync(existingData, cancellationToken);
    }

    public async ValueTask<ProductCategoryResponse?> GetByIdAsync(short id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        var response = result is not null ? result.Adapt<ProductCategoryResponse>() : null;
        return response;
    }

    public async ValueTask<IEnumerable<Lookup<short>>> GetLookup(Expression<Func<ProductCategory, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await _repository.Query().Where(predicate).Select(x => new Lookup<short>(x.Id, x.CategoryName)).ToListAsync();
        return result;
    }

    public async ValueTask<bool> IsExistsAsync(short id, CancellationToken cancellationToken = default)
        => await _repository.Query().AnyAsync(x => x.Id == id, cancellationToken);

    public async ValueTask<ProductCategoryResponse> UpdateAsync(short id, ProductCategoryRequest user, CancellationToken cancellationToken = default)
    {
        ProductCategoryValidator validator = new(_repository, id);
        await validator.ValidateAndThrowAsync(user, cancellationToken);


        var entity = user.Adapt<ProductCategory>();
        _defaultValueInjector.InjectUpdatingAudit<ProductCategory,short>(entity);
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result is null) return null;


        var response = entity.Adapt<ProductCategoryResponse>();
        return response;
    }


    public async ValueTask<IEnumerable<ProductCategoryListResponse>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await _repository.Query()
           .Select(x => new ProductCategoryListResponse(x.Id, x.CategoryName, x.Description, x.Status))
           .ToListAsync(cancellationToken);
        return response;
    }

    public async ValueTask<PaginationResult<ProductCategoryListResponse>> PaginationListAsync(PaginationQuery requestQuery, CancellationToken cancellationToken = default)
    {

        Expression<Func<ProductCategory, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(requestQuery.OpenText) && !string.IsNullOrWhiteSpace(requestQuery.OpenText))
        {
            predicate = obj => obj.CategoryName.ToLower().Contains(requestQuery.OpenText.ToLower())
                            || obj.Description.ToLower().Contains(requestQuery.OpenText.ToLower());
        }

        Expression<Func<ProductCategory, ProductCategoryListResponse>>? selector = x => new ProductCategoryListResponse(x.Id, x.CategoryName, x.Description, x.Status);

        return await _repository.PaginationQuery(paginationQuery: requestQuery, predicate: predicate, selector: selector, cancellationToken);
    }

}
