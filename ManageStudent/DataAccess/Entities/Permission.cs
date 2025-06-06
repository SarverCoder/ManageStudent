﻿namespace ManageStudent.DataAccess.Entities
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public string Name { get; set; }

        public virtual List<RolePermission> RolePermissions { get; set; } = new();
    }
}
