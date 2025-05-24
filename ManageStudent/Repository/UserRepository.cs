using ManageStudent.DataAccess;
using ManageStudent.DataAccess.Entities;
using ManageStudent.Repository.Interfaces;

namespace ManageStudent.Repository
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository;
   
}
