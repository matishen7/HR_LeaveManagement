using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestList;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

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
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.LeaveTypeId);

            await mapper.Map(request, leaveRequest);


            // convert data and map to DTO object

            var data = mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // return list of DTO objects data

            return data;
        }
    }
}
