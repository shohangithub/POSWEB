using Persistence.Context;

namespace Infrastructure.GraphQLSchema
{
    public class ProductCategoryPayloads
    {
        public short Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class Query
    {
        public string Test(string name = "World") => $"Hello, {name}";

        public List<ProductCategory> GetProductCategorys([Service] ApplicationDbContext context)
        {
            try
            {
                var result = context.ProductCategories.ToList();
                return result; //
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription($"Gets the queryable ProductCategory.")]
        //public IQueryable<ProductCategory> GetProductCategorys([Service] ApplicationDbContext context)
        //{
        //    return context.ProductCategories.AsQueryable();
        //}

        //[UseOffsetPaging]
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription($"Gets off set pagination data the queryable ProductCategory.")]
        //public IQueryable<ProductCategory> GetOffSetPageProductCategory([Service] ApplicationDbContext context)
        //{
        //    return context.ProductCategories.AsQueryable();
        //}

        //[UsePaging]
        //[UseProjection]
        //[UseFiltering]
        //[UseSorting]
        //[GraphQLDescription($"Gets Cursor pagination data the queryable ProductCategory.")]
        //public IQueryable<ProductCategory> GetCursorPageProductCategory([Service] ApplicationDbContext context)
        //{
        //    return context.ProductCategories.AsQueryable();
        //}

        //[GraphQLDescription($"Get ProductCategory info by id.")]
        //public async ValueTask<ProductCategory> GetProductCategory(short id, [Service] ApplicationDbContext context, CancellationToken cancellationToken = default)
        //{
        //    return await context.ProductCategories.FirstAsync(x => x.Id == id, cancellationToken);
        //}

    }
}
