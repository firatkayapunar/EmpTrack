using EmpTrack.Application.Common.Results;
using EmpTrack.Application.Features.Auth.Commands;
using EmpTrack.Application.Features.Auth.Responses;
using EmpTrack.Application.Interfaces.Auth;
using EmpTrack.Application.Interfaces.Repositories;
using EmpTrack.Domain.Entities;
using MediatR;

namespace EmpTrack.Application.Features.Auth.Handlers
{
    public sealed class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ServiceResult<LoginResponseDto>>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _jwtTokenService;

        public RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, ITokenService jwtTokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<ServiceResult<LoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (existingToken is null)
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "Invalid refresh token.");

            if (existingToken.IsRevoked)
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "Refresh token has already been revoked.");

            if (existingToken.IsExpired)
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "Refresh token has expired.");

            var user = await _userRepository.GetByIdAsync(existingToken.UserId);

            if (user is null)
                return ServiceResult<LoginResponseDto>.Fail(ResultCode.Unauthorized, "User not found.");

            existingToken.IsRevoked = true;

            var newAccessTokenResult = _jwtTokenService.GenerateAccessToken(user);

            var newRefreshTokenResult = _jwtTokenService.GenerateRefreshToken();

            var newTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshTokenResult.Token,
                ExpiresAt = newRefreshTokenResult.ExpiresAt
            };

            await _refreshTokenRepository.AddAsync(newTokenEntity);
            await _refreshTokenRepository.SaveChangesAsync();

            var response = new LoginResponseDto(
                user.Username,
                newAccessTokenResult.Token,
                newRefreshTokenResult.Token,
                newAccessTokenResult.ExpiresAt
            );

            return ServiceResult<LoginResponseDto>.Success(response, ResultCode.Success);
        }
    }
}
