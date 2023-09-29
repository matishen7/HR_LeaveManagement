using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class LeaveTypeDto
    {
        public int DefaultDays { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
