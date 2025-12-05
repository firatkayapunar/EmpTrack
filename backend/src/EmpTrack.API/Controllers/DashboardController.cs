using EmpTrack.Application.Features.Dashboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [Authorize]
    [Route("api/dashboard")]
    public class DashboardController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats() => CreateActionResult(await _mediator.Send(new GetDashboardStatsQuery()));
    }
}
