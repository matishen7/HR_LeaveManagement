using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<int>
    {
        public int LeaveTypeId { get; set; }
    }
}
