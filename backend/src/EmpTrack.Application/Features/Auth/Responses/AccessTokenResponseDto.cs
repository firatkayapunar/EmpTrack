namespace EmpTrack.Application.Features.Auth.Responses
{
    public record AccessTokenResponseDto(string Token, DateTime ExpiresAt);
}
