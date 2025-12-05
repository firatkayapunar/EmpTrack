using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Dtos;
using EmpTrack.Application.Features.Employees.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, ServiceResult<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<EmployeeDto>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetWithDetailsAsync(request.Id);

            if (employee is null)
                return ServiceResult<EmployeeDto>.Fail(ResultCode.NotFound, "Employee not found.");

            var dto = _mapper.Map<EmployeeDto>(employee);

            return ServiceResult<EmployeeDto>.Success(dto);
        }
    }
}
