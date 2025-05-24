using ManageStudent.DataAccess.Entities;

namespace ManageStudent.Repository.Interfaces
{
    public interface IUserRepository : IRepository<DataAccess.Entities.User>
    {
        Task<DataAccess.Entities.User> GetByUsernameAsync(string username);
    }
}
