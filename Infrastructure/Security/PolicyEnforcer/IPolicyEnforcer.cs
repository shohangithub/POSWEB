using Application.Common;
using ErrorOr;
using Persistence.Authentication.CurrentUserContext;
namespace Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize(
        IAuthorizeableRequest request,
        CurrentUser currentUser,
        string policy);
}
