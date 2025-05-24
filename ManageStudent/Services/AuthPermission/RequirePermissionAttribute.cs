using ManageStudent.DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ManageStudent.Services.AuthPermission
{
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(params PermissionType[] permissions)
        {
            Policy = string.Join(",", permissions.Select(p => p.ToString()));
        }
    }
}
