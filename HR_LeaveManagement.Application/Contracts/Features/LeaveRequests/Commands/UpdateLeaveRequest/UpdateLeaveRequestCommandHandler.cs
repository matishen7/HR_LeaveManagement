using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Email;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Models.Email;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Domain;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestApprovalCommand, Unit>
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
        public async Task<Unit> Handle(UpdateLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
        {
            // query database
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            if (leaveRequest == null)
                throw new NotFoundException(nameof(leaveRequest), request.Id);

            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);
            if (leaveType == null)
                throw new NotFoundException(nameof(leaveType), request.LeaveTypeId);

            var validator = new UpdateLeaveRequestCommandValidator(leaveRequestRepository, leaveTypeRepository);
            var validationResult = validator.ValidateAsync(request);
            if (validationResult.Result.Errors.Any())
            {
                logger.LogWarning("Validation errors in update request for {0} and {1}", nameof(LeaveRequest), request.Id);
                throw new BadRequestException("Invalid LeaveType", validationResult.Result);
            }

            mapper.Map(request, leaveRequest);
            await leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {
                var email = new EmailMessage()
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate} and {request.EndDate} has been updated successfully.",
                    Subject = "Leave Request Submitted"
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
