using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.DeleteLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.UpdateLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR_LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveTypeController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<LeaveTypeController>
        [HttpGet]
        public async Task<List<LeaveTypeDto>> Get()
        {
            return await mediator.Send(new GetLeaveTypeListQuery());
        }

        // GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            var getLeaveTypeDetailsQuery = new GetLeaveTypeDetailsQuery(id);
            var response = await mediator.Send(getLeaveTypeDetailsQuery);
            return Ok(response);
        }

        // POST api/<LeaveTypeController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveTypeCommand leaveType)
        {
            var response = await mediator.Send(leaveType);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveTypeController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveTypeCommand updateLeaveTypeCommand)
        {
            await mediator.Send(updateLeaveTypeCommand);
            return NoContent();
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteLeaveTypeCommand = new DeleteLeaveTypeCommand() { Id = id };
            await mediator.Send(deleteLeaveTypeCommand);
            return NoContent();
        }
    }
}
