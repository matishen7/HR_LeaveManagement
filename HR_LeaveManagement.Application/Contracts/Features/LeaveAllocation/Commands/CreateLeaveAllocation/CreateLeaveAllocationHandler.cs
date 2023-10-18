using AutoMapper;
using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> logger;

        public CreateLeaveAllocationHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IAppLogger<CreateLeaveTypeCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.logger = logger;
            {
            }
        }

        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
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

            return Unit.Value;
        }
    }
}
