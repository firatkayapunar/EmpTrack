using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Commands
{
    public record CreateTitleCommand(string Name, string? Description) : IRequest<ServiceResult<TitleDto>>;
}
