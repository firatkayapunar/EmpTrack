using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Dtos;
using EmpTrack.Application.Features.Departments.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Handlers
{
    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdQuery, ServiceResult<DepartmentDto>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetDepartmentByIdQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DepartmentDto>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult<DepartmentDto>.Fail(ResultCode.NotFound, "Department not found.");

            var dto = _mapper.Map<DepartmentDto>(entity);

            return ServiceResult<DepartmentDto>.Success(dto);
        }
    }
}
