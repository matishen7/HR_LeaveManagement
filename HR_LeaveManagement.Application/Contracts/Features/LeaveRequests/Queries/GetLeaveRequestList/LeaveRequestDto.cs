using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestList
{
    public class LeaveRequestDto
    {
        public LeaveTypeDto? LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime? DateRequested { get; set; }
        public string? RequestComments { get; set; }
        public bool Approved { get; set; }
        public bool Cancelled { get; set; }
        public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}