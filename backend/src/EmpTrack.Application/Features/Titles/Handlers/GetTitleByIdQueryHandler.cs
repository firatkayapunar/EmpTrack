using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Dtos;
using EmpTrack.Application.Features.Titles.Queries;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Handlers
{
    public class GetTitleByIdQueryHandler : IRequestHandler<GetTitleByIdQuery, ServiceResult<TitleDto>>
    {
        private readonly ITitleRepository _repository;
        private readonly IMapper _mapper;

        public GetTitleByIdQueryHandler(ITitleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<TitleDto>> Handle(GetTitleByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult<TitleDto>.Fail(ResultCode.NotFound, "Title not found.");

            var dto = _mapper.Map<TitleDto>(entity);

            return ServiceResult<TitleDto>.Success(dto);
        }
    }
}
