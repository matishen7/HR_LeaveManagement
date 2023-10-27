using FluentValidation;
using HR_LeaveManagement.Application.Contracts.Persistence;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public UpdateLeaveRequestCommandValidator(ILeaveRequestRepository leaveRequestRepository,
            ILeaveTypeRepository leaveTypeRepository)
        {
            Include(new BaseLeaveRequestValidator(leaveTypeRepository));

            RuleFor(p => p.Id)
                .NotNull()
                .MustAsync(LeaveRequestMustExist)
                .WithMessage("{PropertyName} must exist.");


            this.leaveRequestRepository = leaveRequestRepository;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
        {
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(id);
            return leaveRequest != null;
        }
    }
}
