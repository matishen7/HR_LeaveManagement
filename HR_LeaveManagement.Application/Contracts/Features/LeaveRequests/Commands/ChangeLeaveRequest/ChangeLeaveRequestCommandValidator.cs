﻿using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.ChangeLeaveRequest
{
    public class ChangeLeaveRequestCommandValidator : AbstractValidator<ChangeLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public ChangeLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository)
        {
            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} must exist.");

            RuleFor(p => p.Approved)
                .NotNull();


            this.leaveRequestRepository = leaveRequestRepository;
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
        {
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(id);
            return leaveRequest != null;
        }
    }
}
