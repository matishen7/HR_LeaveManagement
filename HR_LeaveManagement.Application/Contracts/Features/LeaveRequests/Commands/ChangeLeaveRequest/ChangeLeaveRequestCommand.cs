using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestCommand : IRequest
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
