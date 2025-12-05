using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Auth.Responses;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Commands
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<ServiceResult<LoginResponseDto>>;
}
