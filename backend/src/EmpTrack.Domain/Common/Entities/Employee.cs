using EmpTrack.Domain.Auditing;

namespace EmpTrack.Domain.Entities
{
    public class Employee : FullAuditableEntity
    {
        public string RegistrationNumber { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string FullName => $"{FirstName} {LastName}";

        public DateTime StartDate { get; set; }

        public bool IsActive { get; set; } = true;

        public string? PhotoPath { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public int TitleId { get; set; }
        public Title Title { get; set; } = null!;

        public AppUser? User { get; set; }
    }
}
