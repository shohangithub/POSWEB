
using Domain.Entitites;

namespace Infrastructure.GraphQLSchema
{
    public record UpdateMutationPayload<T>(T? Data, bool IsSuccess = true, string Message = "") where T : struct;
    public record AddMutationPayload<T>(T? Data, bool IsSuccess = true, string Message = "") where T : struct;
    public record DeleteMutationPayload<T>(T Id, bool IsSucess = true, string Message = "");


    public record ProductCategoryPayload(ProductCategory? model);
    public record UserPayload(User? model);
    public record ProductPayload(Product? model);



}
