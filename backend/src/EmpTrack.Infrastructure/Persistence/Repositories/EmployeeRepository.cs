using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpTrack.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<Employee?> GetByRegistrationNumberAsync(string registrationNumber)
        {
            return await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.RegistrationNumber == registrationNumber);
        }

        public async Task<Employee?> GetWithDetailsAsync(int id)
        {
            return await _context.Employees.Include(x => x.Department).Include(x => x.Title).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Employee>> GetAllWithDetailsAsync()
        {
            return await _context.Employees.Include(x => x.Department).Include(x => x.Title).AsNoTracking().ToListAsync();
        }
    }
}
