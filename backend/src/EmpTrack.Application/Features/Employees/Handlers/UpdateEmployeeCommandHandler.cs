using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Features.Employees.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, ServiceResult<EmployeeDto>>
{
    private readonly IEmployeeRepository _repository;
    private readonly IMapper _mapper;

    public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ServiceResult<EmployeeDto>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _repository.GetWithDetailsAsync(request.Id);

        if (employee is null)
            return ServiceResult<EmployeeDto>.Fail(ResultCode.NotFound, "Employee not found.");

        employee.RegistrationNumber = request.RegistrationNumber;
        employee.FirstName = request.FirstName;
        employee.LastName = request.LastName;
        employee.DepartmentId = request.DepartmentId;
        employee.TitleId = request.TitleId;
        employee.StartDate = request.StartDate;
        employee.IsActive = request.IsActive;

        _repository.Update(employee);
        await _repository.SaveChangesAsync();

        var dto = _mapper.Map<EmployeeDto>(employee);

        return ServiceResult<EmployeeDto>.Success(dto);
    }
}
