using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.CreateLeaveRequest
{
    public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest
    {
        public string RequestComments { get; set; } = string.Empty;
    }
}
