using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public UpdateLeaveAllocationCommandValidator(
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository)
        {
            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater than {ComparisonValue}");

            RuleFor(p => p.Period)
                .GreaterThanOrEqualTo(DateTime.Now.Year)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist)
                .WithMessage("{PropertyName} does not exist.");

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveAllocationExists)
                .WithMessage("{PropertyName} must be present.");

            this.leaveTypeRepository = leaveTypeRepository;
            this.leaveAllocationRepository = leaveAllocationRepository;
        }

        private async Task<bool> LeaveAllocationExists(int id, CancellationToken token)
        {
            var allocation = await leaveAllocationRepository.GetLeaveAllocationWithDetails(id);
            return allocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            var leaveType = await leaveTypeRepository.GetByIdAsync(id);
            return leaveType != null;
        }
    }
}
