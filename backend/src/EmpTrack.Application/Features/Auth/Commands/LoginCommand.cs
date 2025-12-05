using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Auth.Responses;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Commands
{
    public record LoginCommand(string Username, string Password) : IRequest<ServiceResult<LoginResponseDto>>;
}
