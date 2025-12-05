using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Departments.Dtos;
using EmpTrack.Application.Features.Departments.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Departments.Handlers
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsQuery, ServiceResult<List<DepartmentDto>>>
    {
        private readonly IDepartmentRepository _repository;
        private readonly IMapper _mapper;

        public GetAllDepartmentsQueryHandler(IDepartmentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<DepartmentDto>>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();

            var dtoList = _mapper.Map<List<DepartmentDto>>(entities);

            return ServiceResult<List<DepartmentDto>>.Success(dtoList);
        }
    }
}
