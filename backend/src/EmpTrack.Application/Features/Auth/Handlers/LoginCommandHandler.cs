using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Auth.Commands;
using EmpTrack.Application.Features.Auth.Responses;
using EmpTrack.Application.Interfaces.Auth;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Handlers
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResult<LoginResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenService _jwtTokenService;

        public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher passwordHasher, ITokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResult<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);

            if (user is null)
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "User not found.");

            // Girilen password ile hash’li password'u karşılaştırıyoruz.
            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "Invalid password.");

            // Daha önce oluşturulmuş refresh token varsa revoke ediyoruz. Böylece aynı kullanıcı için tek aktif token politikası uygulanıyor
            var oldToken = await _refreshTokenRepository.GetByUserIdAsync(user.Id);

            if (oldToken is not null && !oldToken.IsRevoked)
                oldToken.IsRevoked = true;

            // Yeni Access Token üretiyoruz.
            var accessTokenResult = _jwtTokenService.GenerateAccessToken(user);

            // Yeni Refresh Token üretiyoruz.
            var refreshTokenResult = _jwtTokenService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshTokenResult.Token,
                ExpiresAt = refreshTokenResult.ExpiresAt
            };

            await _refreshTokenRepository.AddAsync(refreshTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            var response = new LoginResponseDto(
                user.Username,
                accessTokenResult.Token,
                refreshTokenResult.Token,
                accessTokenResult.ExpiresAt
            );
            
            // Başarılı login sonucunu client'a döndürüyoruz.
            return ServiceResult<LoginResponseDto>.Success(response, ResultCode.Success);
        }
    }
}
