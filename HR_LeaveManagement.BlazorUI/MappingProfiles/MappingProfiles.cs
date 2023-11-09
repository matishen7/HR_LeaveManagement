using AutoMapper;
using HR_LeaveManagement.BlazorUI.Models;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() {
        
            CreateMap<LeaveTypeDto, LeaveTypeVM>().ReverseMap();
        }

    }
}
