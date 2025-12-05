using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        { }
    }
}
