using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> logger;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IAppLogger<CreateLeaveAllocationCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.logger = logger;
        }

        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // validate request
            var validator = new CreateLeaveAllocationCommandValidator(leaveTypeRepository);
            var validationResult = validator.ValidateAsync(request);
            if (validationResult.Result.Errors.Any())
            {
                logger.LogWarning("Validation errors in create request for {0} and {1}", nameof(LeaveAllocation), request);
                throw new BadRequestException("Invalid LeaveAllocation", validationResult.Result);
            }

            var leaveType = await leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            //Get employees
            //Get Period

            // assign allocations
            var leaveAllocation = mapper.Map<Domain.LeaveAllocation>(request);
            await leaveAllocationRepository.CreateAsync(leaveAllocation);

            // return id

            return leaveAllocation.Id;
        }
    }
}
