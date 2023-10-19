using AutoMapper;
using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.UpdateLeaveType;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> logger;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IAppLogger<UpdateLeaveAllocationCommandHandler> logger)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // validate request

            var validator = new UpdateLeaveAllocationCommandValidator(leaveTypeRepository, leaveAllocationRepository);
            var validationResult = validator.ValidateAsync(request);
            if (validationResult.Result.Errors.Any())
            {
                logger.LogWarning("Validation errors in update request for {0} and {1}", nameof(LeaveAllocation), request.Id);
                throw new BadRequestException("Invalid Leave Allocation", validationResult.Result);
            }

            //get allocation
            var allocation = await leaveAllocationRepository.GetByIdAsync(request.Id);
            if (allocation == null)
            {
                throw new NotFoundException(nameof(allocation), request.Id);
            }
            // convert  request to domain object

            mapper.Map(request, allocation);

            // update db
            await leaveAllocationRepository.UpdateAsync(allocation);

            // return id

            return Unit.Value;
        }
    }
}
