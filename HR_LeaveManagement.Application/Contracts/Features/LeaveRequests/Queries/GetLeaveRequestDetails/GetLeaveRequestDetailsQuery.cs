﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDto>
    {
        public int Id { get; set; }
    }
}
