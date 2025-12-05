using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpTrack.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Username)
                   .IsUnique();

            builder.Property(x => x.Username)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.PasswordHash)
                   .IsRequired();

            builder.Property(x => x.Role)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.HasOne(x => x.Employee)
                   .WithOne(x => x.User)
                   .HasForeignKey<AppUser>(x => x.EmployeeId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
