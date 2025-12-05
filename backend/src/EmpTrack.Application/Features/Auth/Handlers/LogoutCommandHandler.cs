using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Auth.Commands;
using EmpTrack.Application.Interfaces.Repositories;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Handlers
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, ServiceResult>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LogoutCommandHandler(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<ServiceResult> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            /*
            Logout işlemi idempotent çalışır; token bulunmasa bile Success dönülür. Böylece gereksiz hata gösterilmez ve token geçerliliği hakkında dışarı bilgi sızdırılmaz.
           
            Not:
            Idempotent, aynı işlemi kaç kere yaparsan yap, sonuç değişmez demektir.
            */
            if (token is null)
                return ServiceResult.Success();

            token.IsRevoked = true;

            await _refreshTokenRepository.SaveChangesAsync();

            return ServiceResult.Success();
        }
    }
}
