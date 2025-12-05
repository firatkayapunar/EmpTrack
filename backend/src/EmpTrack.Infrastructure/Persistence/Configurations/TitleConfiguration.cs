using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmpTrack.Infrastructure.Persistence.Configurations
{
    public class TitleConfiguration : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.ToTable("Titles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Description)
                   .HasMaxLength(500);

            builder.HasMany(x => x.Employees)
                   .WithOne(x => x.Title)
                   .HasForeignKey(x => x.TitleId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
