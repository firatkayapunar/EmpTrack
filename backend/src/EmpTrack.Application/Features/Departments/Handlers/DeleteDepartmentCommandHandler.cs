using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Handlers
{
    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, ServiceResult>
    {
        private readonly IDepartmentRepository _repository;

        public DeleteDepartmentCommandHandler(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult.Fail(ResultCode.NotFound, "Department not found.");

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success(ResultCode.NoContent);
        }
    }
}
