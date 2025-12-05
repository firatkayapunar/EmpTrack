using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Commands;
using EmpTrack.Application.Features.Departments.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Handlers
{
    public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, ServiceResult<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDepartmentCommandHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DepartmentDto>> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult<DepartmentDto>.Fail(ResultCode.NotFound, "Department not found.");

            entity.Name = request.Name;
            entity.Description = request.Description;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<DepartmentDto>(entity);

            return ServiceResult<DepartmentDto>.Success(dto);
        }
    }
}
