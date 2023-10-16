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
            return await mediator.Send(new GetLeaveTypeQuery());
        }

        // GET api/<LeaveTypeController>/5
        [HttpGet("{id}")]
        public async Task<LeaveTypeDetailsDto> Get(int id)
        {
            var getLeaveTypeDetailsQuery = new GetLeaveTypeDetailsQuery(id);
            return await mediator.Send(getLeaveTypeDetailsQuery);
        }

        // POST api/<LeaveTypeController>
        [HttpPost]
        public async Task<int> Post([FromBody] CreateLeaveTypeCommand createLeaveTypeCommand)
        {
            var createLeaveTypeResponse = await mediator.Send(createLeaveTypeCommand);
            return createLeaveTypeResponse;
        }

        // PUT api/<LeaveTypeController>/5
        [HttpPut("{id}")]
        public async Task<Unit> Put(int id, [FromBody] UpdateLeaveTypeCommand updateLeaveTypeCommand)
        {
            updateLeaveTypeCommand.Id = id;
            return await mediator.Send(updateLeaveTypeCommand);
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("{id}")]
        public async Task<Unit> Delete(int id)
        {
            var deleteLeaveTypeCommand = new DeleteLeaveTypeCommand() { Id = id};
            return await mediator.Send(deleteLeaveTypeCommand);
        }
    }
}
