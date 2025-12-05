using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Titles.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Titles.Handlers
{
    public class DeleteTitleCommandHandler : IRequestHandler<DeleteTitleCommand, ServiceResult>
    {
        private readonly ITitleRepository _repository;

        public DeleteTitleCommandHandler(ITitleRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult> Handle(DeleteTitleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);

            if (entity == null)
                return ServiceResult.Fail(ResultCode.NotFound, "Title not found.");

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();

            return ServiceResult.Success(ResultCode.NoContent);
        }
    }
}
