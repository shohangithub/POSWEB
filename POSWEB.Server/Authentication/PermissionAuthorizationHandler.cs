using Microsoft.AspNetCore.Authorization;
namespace POSWEB.Server.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var role = context.User.Claims.FirstOrDefault(x => x.Type == CustomClaims.Role)?.Value;


        //if (requirement.Permission == role )
        //{
        //    context.Succeed(requirement);
        //}
        if (role == requirement.Permission)
        {
            context.Succeed(requirement);
        }
        else if (role == nameof(Roles.Admin) && (
               requirement.Permission == nameof(Roles.Admin)
            || requirement.Permission == nameof(Roles.Standard))
            )
        {
            context.Succeed(requirement);
        }
        else if (role == nameof(Roles.MasterAdmin) && (
               requirement.Permission == nameof(Roles.MasterAdmin)
            || requirement.Permission == nameof(Roles.Admin)
            || requirement.Permission == nameof(Roles.Standard))
            )
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}
