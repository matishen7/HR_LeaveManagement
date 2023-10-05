using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailsDto>
    {
        private readonly IMapper mapper;
        private readonly ILeaveTypeRepository leaveTypeRepository;

        public GetLeaveTypeDetailsHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            this.mapper = mapper;
            this.leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            // query database
            var leaveTypeDetails = await leaveTypeRepository.GetByIdAsync(request.Id);

            // validate if record exists

            if (leaveTypeDetails == null)
            {
                throw new NotFoundException(nameof(leaveTypeDetails), request);
            }


            // convert data and map to DTO object

            var data = mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);

            // return list of DTO objects data

            return data;
        }
    }
}
