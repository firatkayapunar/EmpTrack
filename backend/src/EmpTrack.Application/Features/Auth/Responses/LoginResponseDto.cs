namespace EmpTrack.Application.Features.Auth.Responses
{
    public record LoginResponseDto(string Username, string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);
}
