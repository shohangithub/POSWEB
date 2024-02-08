using Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Persistence.Authentication;

public class PermissionAttribute(ERoles roles) : AuthorizeAttribute(roles.ToString()) //roles use as policy
{
}
