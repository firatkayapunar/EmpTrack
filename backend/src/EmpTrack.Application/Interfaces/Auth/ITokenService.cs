using EmpTrack.Application.Features.Auth.Responses;
using EmpTrack.Domain.Entities;

namespace EmpTrack.Application.Interfaces.Auth
{
    public interface ITokenService
    {
        AccessTokenResponseDto GenerateAccessToken(AppUser user);

        RefreshTokenResponseDto GenerateRefreshToken();
    }
}
