using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR_LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<LeaveAllocationController>
        [HttpGet]
        public async Task<List<LeaveAllocationDto>> Get()
        {
            return await mediator.Send(new GetLeaveAllocationListQuery());
        }

        // GET api/<LeaveAllocationController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
        {
            var getLeaveAllocationDetailsQuery = new GetLeaveAllocationDetailsQuery().Id = id;
            var response = await mediator.Send(getLeaveAllocationDetailsQuery);
            return Ok(response);
        }

        // POST api/<LeaveAllocationController>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveAllocationController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveAllocationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteLeaveAllocationCommand = new DeleteLeaveAllocationCommand().Id = id;
            await mediator.Send(deleteLeaveAllocationCommand);
            return NoContent();
        }
    }
}
