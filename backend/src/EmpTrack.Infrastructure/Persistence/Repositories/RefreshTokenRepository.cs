using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmpTrack.Infrastructure.Persistence.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        { }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens.Where(r => r.Token == token && !r.IsRevoked && r.ExpiresAt > DateTime.UtcNow).FirstOrDefaultAsync();
        }

        public async Task<RefreshToken?> GetByUserIdAsync(int userId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.ExpiresAt > DateTime.UtcNow && !x.IsRevoked).FirstOrDefaultAsync();
        }
    }
}
