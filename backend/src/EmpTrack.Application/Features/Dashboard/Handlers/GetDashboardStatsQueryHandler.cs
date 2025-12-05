using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Dashboard.Dtos;
using EmpTrack.Application.Features.Dashboard.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Dashboard.Handlers
{
    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, ServiceResult<DashboardStatsDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ITitleRepository _titleRepository;

        public GetDashboardStatsQueryHandler(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository, ITitleRepository titleRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _titleRepository = titleRepository;
        }

        public async Task<ServiceResult<DashboardStatsDto>> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync();
            var departments = await _departmentRepository.GetAllAsync();
            var titles = await _titleRepository.GetAllAsync();

            var activeEmployees = employees.Count(x => x.IsActive);
            var passiveEmployees = employees.Count - activeEmployees;

            var dto = new DashboardStatsDto(
                TotalEmployees: employees.Count,
                TotalDepartments: departments.Count,
                TotalTitles: titles.Count,
                ActiveEmployees: activeEmployees,
                PassiveEmployees: passiveEmployees
            );

            return ServiceResult<DashboardStatsDto>.Success(dto);
        }
    }
}
