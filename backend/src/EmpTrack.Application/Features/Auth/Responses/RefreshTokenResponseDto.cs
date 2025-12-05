namespace EmpTrack.Application.Features.Auth.Responses
{
    public record RefreshTokenResponseDto(string Token, DateTime ExpiresAt);
}
