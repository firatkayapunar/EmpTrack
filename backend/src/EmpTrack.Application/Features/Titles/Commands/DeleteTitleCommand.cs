using EmpTrack.Application.Common.Results;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Commands
{
    public record DeleteTitleCommand(int Id) : IRequest<ServiceResult>;
}
