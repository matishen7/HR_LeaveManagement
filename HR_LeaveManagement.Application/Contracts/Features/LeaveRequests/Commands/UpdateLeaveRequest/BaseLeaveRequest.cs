namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class BaseLeaveRequest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LeaveTypeId { get; set; }
    }
}