using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestList;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
    {
    }
}
