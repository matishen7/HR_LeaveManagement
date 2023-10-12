using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> logger;

        public CreateLeaveTypeCommandHandler(IMapper mapper, 
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<CreateLeaveTypeCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate request
            var validator = new CreateLeaveTypeCommandValidator(leaveTypeRepository);
            var validationResult = validator.ValidateAsync(request);
            if (validationResult.Result.Errors.Any())
            {
                logger.LogWarning("Validation errors in create request for {0} and {1}", nameof(LeaveType), request);
                throw new BadRequestException("Invalid LeaveType", validationResult.Result);
            }
            
            // convert  request to domain object
            var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

            // add to db
            await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // return id

            return leaveTypeToCreate.Id;
        }
    }
}
