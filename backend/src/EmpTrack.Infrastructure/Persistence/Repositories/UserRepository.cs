using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpTrack.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<AppUser>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<AppUser?> GetByUsernameAsync(string username)
        {
            return await _dbSet.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
