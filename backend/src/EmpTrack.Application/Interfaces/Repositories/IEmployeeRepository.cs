using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByRegistrationNumberAsync(string registrationNumber);
        Task<Employee?> GetWithDetailsAsync(int id);
        Task<List<Employee>> GetAllWithDetailsAsync();
    }
}
