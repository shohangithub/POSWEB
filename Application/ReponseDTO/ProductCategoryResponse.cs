namespace Application.ReponseDTO;

public record ProductCategoryResponse(short Id, string CategoryName, string Description, bool IsActive, string Status);
public record ProductCategoryListResponse(short Id, string CategoryName, string? Description, string Status);
