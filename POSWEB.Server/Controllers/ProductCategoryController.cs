namespace POSWEB.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
[Permission(ERoles.Admin)]
public class ProductCategoryController : ControllerBase
{
    private readonly IProductCategoryService _productCategoryService;

    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductCategoryListResponse>> GetProductCategories(CancellationToken cancellationToken)
    {
        return await _productCategoryService.ListAsync(cancellationToken);
    }

    [HttpGet]
    [Route("Lookup")]
    public async Task<IEnumerable<Lookup<short>>> GetLookup(CancellationToken cancellationToken)
    {
        Expression<Func<ProductCategory, bool>> predicate = x=>x.IsActive ==x.IsActive;
        return await _productCategoryService.GetLookup(predicate, cancellationToken);
    }


    [HttpGet]
    [Route("GetWithPagination")]
    public async Task<PaginationResult<ProductCategoryListResponse>> GetWithPagination([FromQuery] PaginationQuery requestQuery, CancellationToken cancellationToken)
    {
        return await _productCategoryService.PaginationListAsync(requestQuery, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductCategoryResponse>> GetProductCategories(short id, CancellationToken cancellationToken)
    {

        var productCategory = await _productCategoryService.GetByIdAsync(id, cancellationToken);
        if (productCategory == null)
        {
            return NotFound();
        }

        return productCategory;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductCategoryResponse>> PutProductCategory(short id, ProductCategoryRequest productCategory)
    {
        var response = await _productCategoryService.UpdateAsync(id, productCategory);
        return response;
    }

    [HttpPost]
    public async Task<ActionResult<ProductCategoryResponse>> PostProductCategory(ProductCategoryRequest productCategory, CancellationToken cancellationToken)
    {
        return await _productCategoryService.AddAsync(productCategory, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async ValueTask<bool> DeleteProductCategory(short id, CancellationToken cancellationToken)
    {
        return await _productCategoryService.DeleteAsync(id, cancellationToken);
    }
    
    [HttpGet("IsProductCategoryExists")]
    public async ValueTask<bool> IsProductCategoryExists([FromQuery] short id, CancellationToken cancellationToken)
    {
        var response = await _productCategoryService.IsExistsAsync(id, cancellationToken);
        return response;
    }
}
