using System.ComponentModel.DataAnnotations.Schema;

namespace POSWEB.Server.GraphQLSchema
{
    public class InputPayload
    {
        public record struct AddProductInput(bool? IsActive, bool? IsUsed, string? Title, Guid? OrganizationId, string? Description);
        public record struct AddProductCategoryInput(string CategoryName, string? Description, bool IsActive);
        public record struct UpdateProductCategoryResponse(string CategoryName, string? Description, bool IsActive);
        public record struct AddUserInput(string UserName, string? Description, bool IsActive);
    }
}
