using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries
{
    public class GetLeaveRequestListCommandHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestDto>>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestListCommandHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            // query database
            var leaveRequests = await leaveRequestRepository.GetLeaveRequestWithDetails();


            // convert data and map to DTO object

            var data = mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            // return list of DTO objects data

            return data;
        }
    {
    }
}
