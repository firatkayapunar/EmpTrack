using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Interfaces.Repositories
{
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        // Login sırasında kullanıcının daha önce üretilmiş refresh token'ı var mı diye kontrol etmek için kullanılır. 
        Task<RefreshToken?> GetByUserIdAsync(int userId);

        // Refresh isteğinde gönderilen token'ın geçerli olup olmadığını doğrulamak için kullanılır.
        Task<RefreshToken?> GetByTokenAsync(string token);
    }
}
