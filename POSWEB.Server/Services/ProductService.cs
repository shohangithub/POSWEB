using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using POSWEB.Server.ServiceContracts;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace POSWEB.Server.Services
{
    public class ProductService<T> : IProductService where T : class
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async ValueTask<List<Product>> ProductListAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async ValueTask<Product> GetByIdAsync(uint id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.Where(ele => ele.Id == id).FirstAsync(cancellationToken);
        }
        public async ValueTask<bool> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async ValueTask<bool> UpdateAsync(uint id, Product product)
        {
            var result = Task.Run(() =>
            {
                var result = _context.Products.Where(x => x.Id < id)
                   .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.ProductCategory, product.ProductCategory)
                        .SetProperty(x => x.ProductCode, product.ProductCode)
                        .SetProperty(x => x.ProductName, product.ProductName)
                        .SetProperty(x => x.IsActive, product.IsActive)
                        .SetProperty(x => x.IsFinishedGoods, product.IsFinishedGoods)
                        .SetProperty(x => x.IsRawMaterial, product.IsRawMaterial)
                        //.SetProperty(x => x.LastUpdatedById, product.CreatedById)
                        .SetProperty(x => x.LastUpdatedTime, DateTime.UtcNow)
           );

                return result > 0;
            });
            return await result;
        }

        //public async Task<bool> UpdatePatchAsync(uint id, JsonPatchDocument<Product> product)
        //{
        //    var result = _context.Products.Where(x => x.Id < id)
        //           .ExecuteUpdate(setters => setters
        //                .SetProperty(x => x.ProductCategory, product.ProductCategory)
        //                .SetProperty(x => x.ProductCode, product.ProductCode)
        //                .SetProperty(x => x.ProductName, product.ProductName)
        //                .SetProperty(x => x.IsActive, product.IsActive)
        //                .SetProperty(x => x.IsFinishedGoods, product.IsFinishedGoods)
        //                .SetProperty(x => x.IsRawMaterial, product.IsRawMaterial)
        //                .SetProperty(x => x.LastUpdatedById, product.CreatedById)
        //                .SetProperty(x => x.LastUpdatedTime, DateTime.UtcNow)
        //   );

        //    return result > 0;
        //}



        public bool DeleteAsync(uint id)
        {
            var result = _context.Products.Where(x => x.Id == id).ExecuteDelete();
            return result > 0;
        }
        public async ValueTask<bool> DeleteAsync(params int[] ids)
        {
            var result = Task.Run(() =>
            {
                var result = _context.Products.Where(x => ids.Contains(x.Id)).ExecuteDelete();
                return result > 0;
            });
            return await result;
        }


        public async ValueTask<bool> isExistsAsync(uint id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

        public async ValueTask<IEnumerable<LookupRecordStruct>> ProductsLookup(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products.Where(predicate).Select(x => new LookupRecordStruct { Id = x.Id, Name = x.ProductName }).ToArrayAsync();
        }
    }
}