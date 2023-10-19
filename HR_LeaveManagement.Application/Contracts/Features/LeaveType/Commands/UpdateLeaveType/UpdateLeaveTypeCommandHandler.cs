using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> logger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate request

            var validator = new UpdateLeaveTypeCommandValidator(leaveTypeRepository);
            var validationResult = validator.ValidateAsync(request);
            if (validationResult.Result.Errors.Any())
            {
                logger.LogWarning("Validation errors in update request for {0} and {1}", nameof(LeaveType), request.Id);
                throw new BadRequestException("Invalid LeaveType", validationResult.Result);
            }
            // convert  request to domain object

            var leaveTypeToUpdate = mapper.Map<Domain.LeaveType>(request);

            // update db
            await leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

            // return id

            return Unit.Value;
        }
    }
}
