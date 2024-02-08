using Domain.Enums;

namespace Persistence.Authentication;

public record struct TokenUser(int id, Guid tenantId, string email, string firstName, string lastName, List<ERoles>? roles = null, List<string>? permissions = null);
