using HR_LeaveManagement.BlazorUI.Models;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeServices
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task<Response<Guid>> CreateLeaveType(LeaveTypeVM vm);
        Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM vm);
        Task<Response<Guid>> DeleteLeaveType(int id);
    }
}
