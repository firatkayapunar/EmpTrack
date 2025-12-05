using EmpTrack.Application.Features.Titles.Commands;
using EmpTrack.Application.Features.Titles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [Authorize]
    [Route("api/titles")]
    public class TitlesController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public TitlesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _mediator.Send(new GetAllTitlesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _mediator.Send(new GetTitleByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTitleCommand command) => CreateActionResult(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTitleCommand command) => CreateActionResult(await _mediator.Send(command with { Id = id }));

        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ServiceResult), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(int id) => CreateActionResult(await _mediator.Send(new DeleteTitleCommand(id)));
    }
}
