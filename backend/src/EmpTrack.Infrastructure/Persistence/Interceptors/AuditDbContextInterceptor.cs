using EmpTrack.Domain.Auditing;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Security.Claims;

namespace EmpTrack.Infrastructure.Persistence.Interceptors
{
    public class AuditDbContextInterceptor(IHttpContextAccessor httpContextAccessor) : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if (dbContext is null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var userName = GetCurrentUsername();

            foreach (var entry in dbContext.ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userName;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userName;
                        break;
                }
            }

            foreach (var entry in dbContext.ChangeTracker.Entries<FullAuditableEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userName;
                    entry.State = EntityState.Modified;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private string GetCurrentUsername()
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                return "system";

            return httpContext.User?.FindFirst(ClaimTypes.Name)?.Value
                   ?? "system";
        }
    }
}
