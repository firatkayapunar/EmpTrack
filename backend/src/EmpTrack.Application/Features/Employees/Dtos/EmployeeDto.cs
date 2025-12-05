namespace EmpTrack.Application.Features.Employees.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public string DepartmentName { get; set; } = null!;
        public string TitleName { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }

        public string? PhotoPath { get; set; }
    }
}
