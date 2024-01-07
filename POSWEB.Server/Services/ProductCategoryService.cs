using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using POSWEB.Server.ServiceContracts;
using System.Linq.Expressions;

namespace POSWEB.Server.Services
{
    public class ProductCategorieservice<T> : IProductCategoryService where T : class
    {
        private readonly ApplicationDbContext _context;
        public ProductCategorieservice(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<List<ProductCategory>> ProductCategoryListAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }
        public async ValueTask<ProductCategory> GetByIdAsync(uint id, CancellationToken cancellationToken = default)
        {
            return await _context.ProductCategories.Where(ele => ele.Id == id).FirstAsync(cancellationToken);
        }
        public async ValueTask<bool> AddAsync(ProductCategory ProductCategory)
        {
            await _context.ProductCategories.AddAsync(ProductCategory);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async ValueTask<bool> UpdateAsync(uint id, ProductCategory ProductCategory)
        {
            var result = Task.Run(() =>
            {
                var result = _context.ProductCategories.Where(x => x.Id < id)
                   .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.CategoryName, ProductCategory.CategoryName)
                        .SetProperty(x => x.IsActive, ProductCategory.IsActive)
                        .SetProperty(x => x.Status, ProductCategory.Status)
                        .SetProperty(x => x.Description, ProductCategory.Description)
                        .SetProperty(x => x.LastUpdatedById, ProductCategory.CreatedById)
                        .SetProperty(x => x.LastUpdatedTime, DateTime.UtcNow)
           );

                return result > 0;
            });
            return await result;
        }

        //public async Task<bool> UpdatePatchAsync(uint id, JsonPatchDocument<ProductCategory> ProductCategory)
        //{
        //    var result = _context.ProductCategories.Where(x => x.Id < id)
        //           .ExecuteUpdate(setters => setters
        //                .SetProperty(x => x.ProductCategoryCategory, ProductCategory.ProductCategoryCategory)
        //                .SetProperty(x => x.ProductCategoryCode, ProductCategory.ProductCategoryCode)
        //                .SetProperty(x => x.ProductCategoryName, ProductCategory.ProductCategoryName)
        //                .SetProperty(x => x.IsActive, ProductCategory.IsActive)
        //                .SetProperty(x => x.IsFinishedGoods, ProductCategory.IsFinishedGoods)
        //                .SetProperty(x => x.IsRawMaterial, ProductCategory.IsRawMaterial)
        //                .SetProperty(x => x.LastUpdatedById, ProductCategory.CreatedById)
        //                .SetProperty(x => x.LastUpdatedTime, DateTime.UtcNow)
        //   );

        //    return result > 0;
        //}



        public bool DeleteAsync(uint id)
        {
            var result = _context.ProductCategories.Where(x => x.Id == id).ExecuteDelete();
            return result > 0;
        }
        public async ValueTask<bool> DeleteAsync(params int[] ids)
        {
            var result = Task.Run(() =>
            {
                var result = _context.ProductCategories.Where(x => ids.Contains(x.Id)).ExecuteDelete();
                return result > 0;
            });
            return await result;
        }


        public async ValueTask<bool> isExistsAsync(uint id)
        {
            return await _context.ProductCategories.AnyAsync(e => e.Id == id);
        }

        public async ValueTask<IEnumerable<LookupRecordShortStruct>> ProductCategoriesLookup(Expression<Func<ProductCategory, bool>> predicate)
        {
            return await _context.ProductCategories.Where(predicate).Select(x => new LookupRecordShortStruct { Id = x.Id, Name = x.CategoryName }).ToArrayAsync();
        }
    }
}