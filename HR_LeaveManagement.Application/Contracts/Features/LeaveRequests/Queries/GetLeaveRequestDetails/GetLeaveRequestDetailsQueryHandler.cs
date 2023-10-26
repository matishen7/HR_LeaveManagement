using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveRequests.Queries.GetLeaveRequestDetails
{
    public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepository;

        public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            this.mapper = mapper;
            this.leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
        {
            // query database
            var leaveRequests = await leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);


            // convert data and map to DTO object

            var data = mapper.Map<LeaveRequestDetailsDto>(leaveRequests);

            // return list of DTO objects data

            return data;


        }
    }
}
