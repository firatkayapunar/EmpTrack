using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Queries
{
    public record GetAllTitlesQuery : IRequest<ServiceResult<List<TitleDto>>>;
}
