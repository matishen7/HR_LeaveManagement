using HR_LeaveManagement.BlazorUI.Models;

namespace HR_LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveTypeServices
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
    }
}
