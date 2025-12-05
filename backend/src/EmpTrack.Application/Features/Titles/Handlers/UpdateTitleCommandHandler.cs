using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Commands;
using EmpTrack.Application.Features.Titles.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Handlers
{
    public class UpdateTitleCommandHandler : IRequestHandler<UpdateTitleCommand, ServiceResult<TitleDto>>
    {
        private readonly ITitleRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTitleCommandHandler(ITitleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<TitleDto>> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult<TitleDto>.Fail(ResultCode.NotFound, "Title not found.");

            entity.Name = request.Name;
            entity.Description = request.Description;

            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<TitleDto>(entity);

            return ServiceResult<TitleDto>.Success(dto);
        }
    }
}
