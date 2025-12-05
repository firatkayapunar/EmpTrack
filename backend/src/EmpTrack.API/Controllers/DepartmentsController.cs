using EmpTrack.Application.Features.Departments.Commands;
using EmpTrack.Application.Features.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [Authorize]
    [Route("api/departments")]
    public class DepartmentsController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _mediator.Send(new GetAllDepartmentsQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _mediator.Send(new GetDepartmentByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command) => CreateActionResult(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentCommand command) => CreateActionResult(await _mediator.Send(command with { Id = id }));

        //[HttpDelete("{id}")]
        //[ProducesResponseType(typeof(ServiceResult), StatusCodes.Status204NoContent)]
        //[ProducesResponseType(typeof(ServiceResult), StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Delete(int id) => CreateActionResult(await _mediator.Send(new DeleteDepartmentCommand(id)));
    }
}
