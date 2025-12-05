using EmpTrack.Domain.Auditing;

namespace EmpTrack.Domain.Entities
{
    public class Department : AuditableEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; } = new List<Employee>();
    }
}
