using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Commands;
using EmpTrack.Application.Features.Departments.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Handlers
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, ServiceResult<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public CreateDepartmentCommandHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DepartmentDto>> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Department
            {
                Name = request.Name,
                Description = request.Description
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<DepartmentDto>(entity);

            return ServiceResult<DepartmentDto>.Success(
                dto,
                ResultCode.Created);
        }
    }
}
