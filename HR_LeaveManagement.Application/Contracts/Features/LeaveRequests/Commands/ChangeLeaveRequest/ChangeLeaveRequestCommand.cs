using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestCommand : IRequest
    {
        public int Id { get; set; }
        public bool Approved { get; set; }
    }
}
