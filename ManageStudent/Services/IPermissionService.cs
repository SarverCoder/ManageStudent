namespace ManageStudent.Services
{
    public interface IPermissionService
    {
        Task<List<string>> GetUserPermissions(int userId);
    }
}
