using HR_LeaveManagement.BlazorUI.Contracts;
using HR_LeaveManagement.BlazorUI.Models;
using HR_LeaveManagement.BlazorUI.Services.Base;

namespace HR_LeaveManagement.BlazorUI.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeServices
    {
        public LeaveTypeService(IClient client) : base(client)
        {
        }

        public Task<Response<Guid>> CreateLeaveType(LeaveTypeVM vm)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Guid>> DeleteLeaveType(int id)
        {
            throw new NotImplementedException();
        }

        public Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            var leaveTypes = await _client.LeaveTypeAllAsync();
            return (List<LeaveTypeVM>)leaveTypes;
        }

        public Task<Response<Guid>> UpdateLeaveType(int id, LeaveTypeVM vm)
        {
            throw new NotImplementedException();
        }
    }
}
