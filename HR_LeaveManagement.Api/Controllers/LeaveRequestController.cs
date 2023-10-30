using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.CancelLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.ChangeLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.DeleteLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.CreateLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestDetails;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR_LeaveManagement.Api.Controllers
{
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator mediator;
        public LeaveRequestController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: LeaveRequestController
        public async Task<List<LeaveRequestDto>> Get()
        {
            return await mediator.Send(new GetLeaveRequestListQuery());
        }

        // GET: LeaveRequestController/Details/5
        [HttpGet("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeaveRequestDetailsDto>> Get(int id)
        {
            var getLeaveRequestDetailsQuery = new GetLeaveRequestDetailsQuery();
            getLeaveRequestDetailsQuery.Id = id;
            var response = await mediator.Send(getLeaveRequestDetailsQuery);
            return Ok(response);
        }


        // POST: LeaveRequestController/Create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post([FromBody] CreateLeaveRequestCommand command)
        {
            var response = await mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = response });
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateApproval([FromBody] UpdateLeaveRequestApprovalCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Change([FromBody] ChangeLeaveRequestCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveRequestController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Cancel([FromBody] CancelLeaveRequestCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveRequestController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleteLeaveRequestCommand = new DeleteLeaveRequestCommand();
            deleteLeaveRequestCommand.Id = id;
            await mediator.Send(deleteLeaveRequestCommand);
            return NoContent();
        }
    }
}
