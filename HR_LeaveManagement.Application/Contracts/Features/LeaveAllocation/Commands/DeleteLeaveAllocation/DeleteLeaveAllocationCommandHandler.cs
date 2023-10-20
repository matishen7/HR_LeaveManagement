using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.DeleteLeaveType;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this.leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {

            // get from db
            var leaveAllocationToDelete = await leaveAllocationRepository.GetByIdAsync(request.Id);

            //validate record exists

            if (leaveAllocationToDelete == null)
            {
                throw new NotFoundException(nameof(leaveAllocationToDelete), request);
            }

            // delete from db
            await leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

            // return id

            return Unit.Value;
        }
    }
}
