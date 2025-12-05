using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Dtos;
using EmpTrack.Application.Features.Titles.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Handlers
{
    public class GetAllTitlesQueryHandler : IRequestHandler<GetAllTitlesQuery, ServiceResult<List<TitleDto>>>
    {
        private readonly ITitleRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTitlesQueryHandler(ITitleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<List<TitleDto>>> Handle(GetAllTitlesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAllAsync();

            var dtoList = _mapper.Map<List<TitleDto>>(entities);

            return ServiceResult<List<TitleDto>>.Success(dtoList);
        }
    }
}
