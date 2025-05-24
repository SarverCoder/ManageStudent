using ManageStudent.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ManageStudent.Services.AuthPermission
{
    public class PermissionAuthorizationPolicyRequirement(PermissionType permissionType) : IAuthorizationRequirement
    {
        public PermissionType Permission { get; set; } = permissionType;
    }
}
