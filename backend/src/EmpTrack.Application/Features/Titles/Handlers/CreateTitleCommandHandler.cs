using AutoMapper;
using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Commands;
using EmpTrack.Application.Features.Titles.Dtos;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Handlers
{
    public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, ServiceResult<TitleDto>>
    {
        private readonly ITitleRepository _repository;
        private readonly IMapper _mapper;

        public CreateTitleCommandHandler(ITitleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<TitleDto>> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = new Title
            {
                Name = request.Name,
                Description = request.Description
            };

            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            var dto = _mapper.Map<TitleDto>(entity);

            return ServiceResult<TitleDto>.Success(dto, ResultCode.Created);
        }
    }
}
