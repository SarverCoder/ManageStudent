using ManageStudent.DataAccess;
using ManageStudent.DataAccess.Entities;
using ManageStudent.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ManageStudent.Repository
{
    public class UserRepository(ApplicationDbContext context) : Repository<User>(context), IUserRepository
    {
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await context.Users
                .Include(r => r.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

        }
    }
}
