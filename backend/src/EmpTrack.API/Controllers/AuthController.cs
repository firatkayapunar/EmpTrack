using EmpTrack.Application.Features.Auth.Commands;
using EmpTrack.Application.Features.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [Route("api/auth")]
    public class AuthController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request) => CreateActionResult(await _mediator.Send(new LoginCommand(request.Username, request.Password)));

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto request) => CreateActionResult(await _mediator.Send(new RefreshTokenCommand(request.RefreshToken)));

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutRequestDto request) => CreateActionResult(await _mediator.Send(new LogoutCommand(request.RefreshToken)));
    }
}
