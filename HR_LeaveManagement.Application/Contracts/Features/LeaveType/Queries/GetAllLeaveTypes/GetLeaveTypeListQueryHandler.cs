using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypeListQueryHandler : IRequestHandler<GetLeaveTypeListQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public GetLeaveTypeListQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeListQuery request, CancellationToken cancellationToken)
        {
            // query database
            var leaveTypes = await leaveTypeRepository.GetAsync();


            // convert data and map to DTO object

            var data = mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            // return list of DTO objects data

            return data;
        }
    }
}
