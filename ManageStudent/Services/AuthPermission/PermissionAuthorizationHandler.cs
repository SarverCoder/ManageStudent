using ManageStudent.DataAccess.Constants;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;

namespace ManageStudent.Services.AuthPermission;

public class PermissionAuthorizationHandler() : AuthorizationHandler<PermissionAuthorizationPolicyRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionAuthorizationPolicyRequirement requirement)
    {
        var permissions = context.User.Claims.Where(c => c.Type == AuthConstants.PermissionClaimType)
            .Select(c => c.Value)
            .ToList();

        if (permissions.Contains(requirement.Permission.ToString()))
        {
            context.Succeed(requirement);
        }
        else
        {
            throw new UnauthorizedAccessException();
        }
    }
}