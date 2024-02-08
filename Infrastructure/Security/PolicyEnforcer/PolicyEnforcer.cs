﻿using Application.Common;
using Application.Contractors.Common.Security;
using ErrorOr;
using Persistence.Authentication.CurrentUserContext;


namespace Infrastructure.Security.PolicyEnforcer
{
    public class PolicyEnforcer : IPolicyEnforcer
    {
        public ErrorOr<Success> Authorize(
            IAuthorizeableRequest request,
            CurrentUser currentUser,
            string policy)
        {
            return policy switch
            {
                Policy.SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
                _ => ErrorOr.Error.Unexpected(description: "Unknown policy name"),
            };
        }

        private static ErrorOr<Success> SelfOrAdminPolicy(IAuthorizeableRequest request, CurrentUser currentUser) =>
            request.UserId == currentUser.Email //|| currentUser.Roles.Contains(Role.Admin)
                ? Result.Success
                : ErrorOr.Error.Unauthorized(description: "Requesting user failed policy requirement");
    }
}
