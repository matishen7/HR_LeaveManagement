using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR_LeaveManagement.Domain;

namespace HR_LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}
