using EmpTrack.Domain.Common;

namespace EmpTrack.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int UserId { get; set; }

        public string Token { get; set; } = default!;

        public DateTime ExpiresAt { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    }
}
