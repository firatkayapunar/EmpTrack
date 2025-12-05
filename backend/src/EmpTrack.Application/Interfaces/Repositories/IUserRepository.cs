using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<AppUser?> GetByUsernameAsync(string username);
    }
}
