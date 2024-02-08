using Persistence.Authentication;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Persistence.Authentication.CurrentUserContext;

public class CurrentUserProvider(IHttpContextAccessor _httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        //_httpContextAccessor.HttpContext.ThrowIfNull();

        var id = int.Parse(GetSingleClaimValue(CustomClaims.Id) ?? "0");
        var tenantId = Guid.Parse(GetSingleClaimValue(CustomClaims.Tenant)?? "00000000-0000-0000-0000-000000000000");
        var firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        var email = GetSingleClaimValue(JwtRegisteredClaimNames.Email);
        var roles = GetClaimValues(CustomClaims.Role);
        //var permissions = GetClaimValues("permissions");
        //var lastName = GetSingleClaimValue(ClaimTypes.Surname);

        return new CurrentUser(id, tenantId, firstName, null, email, null, roles);
    }

    private List<string>? GetClaimValues(string claimType) =>
        _httpContextAccessor.HttpContext?.User?.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private string? GetSingleClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == claimType)?.Value;


}
