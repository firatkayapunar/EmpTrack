using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpTrack.Infrastructure.Persistence.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.RegistrationNumber)
                   .IsUnique();

            builder.Property(x => x.RegistrationNumber)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.FirstName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.LastName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.PhotoPath)
                   .HasMaxLength(400);

            builder.Property(x => x.StartDate)
                   .IsRequired();

            builder.Property(x => x.IsActive)
                   .IsRequired();

            builder.HasOne(x => x.Department)
                   .WithMany(x => x.Employees)
                   .HasForeignKey(x => x.DepartmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Title)
                   .WithMany(x => x.Employees)
                   .HasForeignKey(x => x.TitleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.User)
                   .WithOne(x => x.Employee)
                   .HasForeignKey<AppUser>(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
