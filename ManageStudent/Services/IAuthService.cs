namespace ManageStudent.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(int userId, string email,string role);
    }
}
