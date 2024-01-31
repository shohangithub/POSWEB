using Domain.Entitites;
using Domain.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
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
    public async ValueTask<UpdateMutationPayload<UpdateProductCategoryResponse>> UpdateProductCategoryAsync(
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
            if (result <= 0) return new UpdateMutationPayload<UpdateProductCategoryResponse>(response, false);
            return new UpdateMutationPayload<UpdateProductCategoryResponse>(response);
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
    public async ValueTask<DeleteMutationPayload<short>> DeleteProductCategoryAsync(
               short id,
               [Service] ApplicationDbContext context,
               CancellationToken cancellationToken
               )
    {
        try
        {
            var result = await context.ProductCategories.Where(x => x.Id == id).ExecuteDeleteAsync(cancellationToken);
            DeleteMutationPayload<short> response = new(id, result > 0, result > 0 ? "ProductCategory deleted successfully" : "ProductCategory delete operation failed");

            return response;
        }
        catch (Exception ex)
        {
            DeleteMutationPayload<short> response = new(id, false, "Operation failed ! internal server error.");

            return response;
        }

    }


    [GraphQLDescription($"Add User Information")]
    public async ValueTask<UserResponse> AddUserAsync(UserRequest input, [Service] IRepository<User, int> _repository, CancellationToken cancellationToken)
    {
        var entity = input.Adapt<User>();
        var result = await _repository.AddAsync(entity, cancellationToken);

        return result ? entity.Adapt<UserResponse>() : input.Adapt<UserResponse>();
    }

    [GraphQLDescription($"Update User Information")]
    public async ValueTask<UserResponse> UpdateUserAsync(
                int id,
                UserRequest input,
                [Service] IRepository<User, int> _repository,
                CancellationToken cancellationToken
                )
    {

        var entity = input.Adapt<User>();
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result is null) return null;


        var response = entity.Adapt<UserResponse>();
        return response;
    }


    [GraphQLDescription($"Delete User Information.")]
    public async ValueTask<DeleteMutationPayload<int>> DeleteUserAsync(
               int id,
               [Service] IRepository<User, int> _repository,
               CancellationToken cancellationToken
               )
    {
        var existingData = await _repository.GetByIdAsync(id, cancellationToken);
        if (existingData is null) throw new ArgumentNullException(nameof(existingData));
        var result = await _repository.DeleteAsync(existingData, cancellationToken);

        DeleteMutationPayload<int> response = new(id, result, result ? "ProductCategory deleted successfully" : "ProductCategory delete operation failed");

        return response;
    }
}
