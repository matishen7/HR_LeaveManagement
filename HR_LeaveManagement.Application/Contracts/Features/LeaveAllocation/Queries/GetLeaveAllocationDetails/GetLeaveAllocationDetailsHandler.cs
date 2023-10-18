using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IAppLogger<GetLeaveTypeDetailsHandler> logger;

        public GetLeaveAllocationDetailsHandler(IMapper mapper, 
            ILeaveAllocationRepository leaveAllocationRepository,
            IAppLogger<GetLeaveTypeDetailsHandler> logger)
        {
            this.mapper = mapper;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.logger = logger;
        }

        public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            // query database
            var leaveAllocationDetails = await leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

            // validate if record exists

            if (leaveAllocationDetails == null)
            {
                throw new NotFoundException(nameof(leaveAllocationDetails), request);
            }


            // convert data and map to DTO object

            var data = mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);

            // return list of DTO objects data
            logger.LogInformation("Get leave allocation details retrieved successfully.");
            return data;
        }
    }
}
