namespace ManageStudent.Services
{
    public class PermissionService : IPermissionService
    {
        public async Task<List<string>> GetUserPermissions(int userId)
        {
            return new List<string>();
        }
    }
}
