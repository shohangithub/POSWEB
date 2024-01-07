using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.Entitites;
using static POSWEB.Server.GraphQLSchema.InputPayload;

namespace POSWEB.Server.GraphQLSchema;

public class Mutations
{

    public async ValueTask<ProductCategoryPayload> AddProductCategoryAsync([Service] ApplicationDbContext context, AddProductCategoryInput input, CancellationToken cancellationToken)
    {
        try
        {
            ProductCategory entity = new()
            {
                CreatedById = 1,
                CategoryName = input.CategoryName,
                Description = input.Description,
                IsActive = input.IsActive,
                CreatedTime = DateTime.Now
            };
            await context.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return new ProductCategoryPayload(entity);
        }
        catch (Exception ex)
        {
            throw;
        }

    }


    [GraphQLDescription($"Update a ProductCategory.")]
    public async ValueTask<UpdateResponsePayload<UpdateProductCategoryResponse>> UpdateProductCategoryAsync(
                short id,
                AddProductCategoryInput input,
                [Service] ApplicationDbContext context,
                CancellationToken cancellationToken
                )
    {
        try
        {
            var result = await context.ProductCategories.Where(x => x.Id == id)
                             .ExecuteUpdateAsync(setters => setters
                                  .SetProperty(x => x.CategoryName, input.CategoryName)
                                  .SetProperty(x => x.Description, input.Description)
                                  .SetProperty(x => x.IsActive, input.IsActive)
                                  .SetProperty(x => x.LastUpdatedById, 1)
                                  .SetProperty(x => x.LastUpdatedTime, DateTime.Now)
                                  , cancellationToken);
            var response = new UpdateProductCategoryResponse(input.CategoryName, input.Description, input.IsActive);
            if (result <= 0) return new UpdateResponsePayload<UpdateProductCategoryResponse>(response, false);
            return new UpdateResponsePayload<UpdateProductCategoryResponse>(response);
        }
        catch (Exception ex)
        {
            throw;
        }


        //var context = _securityContextProvider.GetSecurityContext();
        //var loginUser = this.GetUser(context);
        //var exData = await service.GetItemAsync<ProductCategory>(obj => obj.ItemId == id);
        //exData = exData.Decrypt();
        //GraphQlPartialUpdate.PartialUpdateFromGraphQLInputType<AddProductCategoryInput, ProductCategory>(input, exData, operationType);
        //exData = exData.Encrypted();
        //exData.LastUpdatedByUser = loginUser;
        //exData.LastUpdatedByUserId = loginUser.ItemId;
        //exData.LastUpdateDate = DateTime.UtcNow;
        //await service.UpdateAsync<ProductCategory>(obj => obj.ItemId == id, exData, cancellationToken);
        //return new ProductCategoryPayload(exData);
    }


    [GraphQLDescription($"Delete a ProductCategory.")]
    public async ValueTask<DeleteMutationPayload> DeleteProductCategoryAsync(
               short id,
               [Service] ApplicationDbContext context,
               CancellationToken cancellationToken
               )
    {
        try
        {
            var result = await context.ProductCategories.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
            DeleteMutationPayload response = new()
            {
                model = new DeleteMutation
                {
                    IsSucess = result > 0,
                    Message = result > 0 ? "ProductCategory deleted successfully" : "ProductCategory delete operation failed"
                }
            };
            return response;
        }
        catch (Exception ex)
        {
            DeleteMutationPayload response = new()
            {
                model = new DeleteMutation
                {
                    IsSucess = false,
                    Message = ex.Message
                }
            };
            return response;
        }

    }
}
