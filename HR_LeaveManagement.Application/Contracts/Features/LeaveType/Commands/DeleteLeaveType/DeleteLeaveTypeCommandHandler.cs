using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {

            // get from db
            var leaveTypeToDelete = await leaveTypeRepository.GetByIdAsync(request.Id);

            //validate record exists

            if (leaveTypeToDelete == null)
            {
                throw new NotFoundException(nameof(leaveTypeToDelete), request);
            }

            // delete from db
            await leaveTypeRepository.DeleteAsync(leaveTypeToDelete);

            // return id

            return Unit.Value;
        }
    }
}
