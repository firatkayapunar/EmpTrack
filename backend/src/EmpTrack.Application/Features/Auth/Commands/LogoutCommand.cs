using EmpTrack.Application.Common.Results;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Commands
{
    public record LogoutCommand(string RefreshToken) : IRequest<ServiceResult>;
}
