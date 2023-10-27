using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Models.Email;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Domain;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.CreateLeaveRequest
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<CreateLeaveRequestCommandHandler> logger;

        public CreateLeaveRequestCommandHandler(IMapper mapper,
            ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IEmailSender emailSender,
            IAppLogger<CreateLeaveRequestCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
            this.emailSender = emailSender;
            this.logger = logger;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // query database
            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.LeaveTypeId);

            var leaveRequest = mapper.Map<LeaveRequest>(request);
            await leaveRequestRepository.CreateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage()
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate} and {request.EndDate} has been created successfully.",
                    Subject = "Leave Request Created"
                };
                await emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                logger.LogWarning("Error occured while sending email " + ex.Message);
            }

            return Unit.Value;
        }
    }
}