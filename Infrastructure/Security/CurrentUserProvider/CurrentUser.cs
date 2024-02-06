namespace Infrastructure.Security.CurrentUserProvider;

public record CurrentUser(
    int Id,
    Guid TenantId,
    string FirstName,
    string LastName,
    string Email,
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles);
