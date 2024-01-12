using Microsoft.AspNetCore.Authorization;

namespace POSWEB.Server.Authentication;

public class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string Permission { get; } = permission;
}
