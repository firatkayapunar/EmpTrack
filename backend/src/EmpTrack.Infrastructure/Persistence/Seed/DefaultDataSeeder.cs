using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpTrack.Infrastructure.Persistence.Seed
{
    public class DefaultDataSeeder : IDataSeeder
    {
        private readonly ApplicationDbContext _context;

        public DefaultDataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            if (!await _context.Departments.IgnoreQueryFilters().AnyAsync())
            {
                _context.Departments.AddRange(
                    new Department { Name = "Human Resources", Description = "HR Department" },
                    new Department { Name = "IT", Description = "Information Technology" },
                    new Department { Name = "Finance", Description = "Accounting & Finance" }
                );
            }

            if (!await _context.Titles.IgnoreQueryFilters().AnyAsync())
            {
                _context.Titles.AddRange(
                    new Title { Name = "Software Developer", Description = "Backend & Frontend Developer" },
                    new Title { Name = "Team Lead", Description = "Technical Team Lead" },
                    new Title { Name = "HR Specialist", Description = "Human Resources Specialist" }
                );
            }

            await _context.SaveChangesAsync();

            var departments = await _context.Departments.ToListAsync();
            var titles = await _context.Titles.ToListAsync();

            if (!await _context.Employees.IgnoreQueryFilters().AnyAsync())
            {
                _context.Employees.AddRange(
                    new Employee
                    {
                        RegistrationNumber = "EMP-1002",
                        FirstName = "Zeynep",
                        LastName = "Kaya",
                        StartDate = DateTime.UtcNow.AddMonths(-8),
                        IsActive = true,
                        DepartmentId = departments.First(d => d.Name == "Human Resources").Id,
                        TitleId = titles.First(t => t.Name == "HR Specialist").Id
                    },
                    new Employee
                    {
                        RegistrationNumber = "EMP-1001",
                        FirstName = "Şevket",
                        LastName = "Yorgun",
                        StartDate = DateTime.UtcNow.AddYears(-1),
                        IsActive = true,
                        DepartmentId = departments.First(d => d.Name == "IT").Id,
                        TitleId = titles.First(t => t.Name == "Software Developer").Id
                    }
                );
            }

            await _context.SaveChangesAsync();

            var employees = await _context.Employees.ToListAsync();

            if (!await _context.Users.IgnoreQueryFilters().AnyAsync())
            {
                _context.Users.AddRange(
                    new AppUser
                    {
                        Username = "zeynep",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("EmpTrack!2025Strong"),
                        Role = "HR",
                        EmployeeId = employees.First().Id
                    }
                );
            }

            await _context.SaveChangesAsync();
        }
    }
}
