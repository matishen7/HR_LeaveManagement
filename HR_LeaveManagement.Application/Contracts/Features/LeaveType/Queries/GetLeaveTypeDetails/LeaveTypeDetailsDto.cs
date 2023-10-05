using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class LeaveTypeDetailsDto
    {
        public int DefaultDays { get; set; }
        public string Name { get; set; } = string.Empty;

        public DateTime? DateCreated { get; set; }
        public int Id { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
