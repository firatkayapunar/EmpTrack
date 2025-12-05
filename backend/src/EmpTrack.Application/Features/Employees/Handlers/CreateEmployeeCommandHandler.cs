using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Features.Employees.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ServiceResult<EmployeeDto>>
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<EmployeeDto>> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = new Employee
            {
                RegistrationNumber = request.RegistrationNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DepartmentId = request.DepartmentId,
                TitleId = request.TitleId,
                StartDate = request.StartDate,
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            var employee = await _repository.GetWithDetailsAsync(entity.Id);

            var dto = _mapper.Map<EmployeeDto>(employee!);

            return ServiceResult<EmployeeDto>.Success(dto, ResultCode.Created);
        }
    }
}
