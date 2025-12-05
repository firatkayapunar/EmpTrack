using EmpTrack.Domain.Entities;
using EmpTrack.Infrastructure.Marker;
using Microsoft.EntityFrameworkCore;

namespace EmpTrack.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<AppUser> Users => Set<AppUser>();
        public DbSet<Department> Departments => Set<Department>();
        public DbSet<Title> Titles => Set<Title>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasQueryFilter(x => !x.IsDeleted);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InfrastructureAssemblyMarker).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
