using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Dashboard.Dtos;
using MediatR;

namespace EmpTrack.Application.Features.Dashboard.Queries
{
    public record GetDashboardStatsQuery : IRequest<ServiceResult<DashboardStatsDto>>;
}
