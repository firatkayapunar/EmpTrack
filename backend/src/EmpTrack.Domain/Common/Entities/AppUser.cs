using EmpTrack.Domain.Auditing;

namespace EmpTrack.Domain.Entities
{
    public class AppUser : AuditableEntity
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = "HR";

        public int? EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
