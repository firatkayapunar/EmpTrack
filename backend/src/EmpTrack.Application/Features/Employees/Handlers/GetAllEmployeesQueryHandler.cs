using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Dtos;
using EmpTrack.Application.Features.Employees.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, ServiceResult<List<EmployeeDto>>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _repository.GetAllWithDetailsAsync();

            var dtos = _mapper.Map<List<EmployeeDto>>(employees);

            return ServiceResult<List<EmployeeDto>>.Success(dtos);
        }
    }
}
