using Microsoft.AspNetCore.Authorization;

namespace POSWEB.Server.Authentication;

public class PermissionAttribute(Roles roles) : AuthorizeAttribute(roles.ToString()) //roles use as policy
{
}
