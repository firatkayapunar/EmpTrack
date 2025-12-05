using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Employees.Handlers
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, ServiceResult>
    {
        private readonly IEmployeeRepository _repository;

        public DeleteEmployeeCommandHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repository.GetByIdAsync(request.Id);

            if (employee is null)
                return ServiceResult.Fail(ResultCode.NotFound, "Employee not found.");

            employee.IsActive = false;

            _repository.Delete(employee);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success(ResultCode.NoContent);
        }
    }
}
