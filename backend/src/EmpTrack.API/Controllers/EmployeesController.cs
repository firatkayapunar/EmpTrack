using EmpTrack.Application.Features.Employees.Commands;
using EmpTrack.Application.Features.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmpTrack.API.Controllers
{
    [Authorize]
    [Route("api/employees")]
    public class EmployeesController : CustomBaseController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _mediator.Send(new GetAllEmployeesQuery()));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _mediator.Send(new GetEmployeeByIdQuery(id)));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command) => CreateActionResult(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmployeeCommand command) => CreateActionResult(await _mediator.Send(command with { Id = id }));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _mediator.Send(new DeleteEmployeeCommand(id)));

        [HttpPost("{id}/photo")]
        public async Task<IActionResult> UploadPhoto(int id, IFormFile photo) => CreateActionResult(await _mediator.Send(new UploadEmployeePhotoCommand(id, photo)));
    }
}
