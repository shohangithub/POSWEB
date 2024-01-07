using POSWEB.Server.Entitites;

namespace POSWEB.Server.GraphQLSchema
{
    public record UpdateResponsePayload<T>(T? UpdatedData, bool IsSuccess = true) where T : struct;
    public record ProductCategoryPayload(ProductCategory? model);
    public record UserPayload(User? model);
    public record ProductPayload(Product? model);


    public record struct DeleteMutation(bool IsSucess, string Message);
    public record struct DeleteMutationPayload(DeleteMutation model);
}
