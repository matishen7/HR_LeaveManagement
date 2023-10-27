using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestList;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Logging;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> logger;

        public UpdateLeaveRequestCommandHandler(IMapper mapper, 
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender,
            IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.emailSender = emailSender;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // query database
            var leaveRequests = await leaveRequestRepository.GetLeaveRequestWithDetails();


            // convert data and map to DTO object

            var data = mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // return list of DTO objects data

            return data;
        }
    }
}
