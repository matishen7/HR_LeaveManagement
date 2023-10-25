using HR_LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestListQuery : IRequest<List<LeaveRequestDto>>
    {
    }
}
