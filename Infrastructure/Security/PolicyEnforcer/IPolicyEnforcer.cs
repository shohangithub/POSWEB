using Application.Common;
using ErrorOr;
using Infrastructure.Security.CurrentUserProvider;

namespace Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize(
        IAuthorizeableRequest request,
        CurrentUser currentUser,
        string policy);
}
