using HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.ChangeLeaveRequest;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Commands.CancelLeaveRequest
{
    public class ChangeLeaveRequestCommandHandler : IRequestHandler<ChangeLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public ChangeLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<Unit> Handle(ChangeLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            // query database
            var leaveRequest = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
            if (leaveRequest == null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            leaveRequest.Approved = request.Approved;
            await leaveRequestRepository.UpdateAsync(leaveRequest);

            return Unit.Value;


        }
    }
}
