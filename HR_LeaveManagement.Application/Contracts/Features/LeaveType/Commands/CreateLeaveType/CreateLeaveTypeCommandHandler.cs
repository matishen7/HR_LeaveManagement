using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate request
            // convert  request to domain object

            var leaveTypeToCreate = mapper.Map<Domain.LeaveType>(request);

            // add to db
            await leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // return id

            return leaveTypeToCreate.Id;
        }
    }
}
