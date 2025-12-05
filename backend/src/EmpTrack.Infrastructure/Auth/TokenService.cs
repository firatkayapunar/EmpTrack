using EmpTrack.Application.Features.Auth.Responses;
using EmpTrack.Application.Interfaces.Auth;
using EmpTrack.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmpTrack.Infrastructure.Auth
{
    public sealed class TokenService : ITokenService
    {
        //_jwtSettings artık appsettings'teki ayarlar ile doldurulmuş durumda.
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public AccessTokenResponseDto GenerateAccessToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenMinutes);

            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer, audience: _jwtSettings.Audience, claims: claims, expires: expiresAt, signingCredentials: credentials);

            Console.WriteLine($"JWT EXPIRES: {expiresAt:yyyy-MM-dd HH:mm:ss} UTC");

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AccessTokenResponseDto(tokenString, expiresAt);
        }

        public RefreshTokenResponseDto GenerateRefreshToken()
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            var expiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);

            return new RefreshTokenResponseDto(token, expiresAt);
        }
    }
}
