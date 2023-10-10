using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR_LeaveManagement.Persistance.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
        {
            await _context.AddRangeAsync(leaveAllocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(q => q.Period == period && q.LeaveTypeId == leaveTypeId && q.EmployeeId == userId);
        }

        public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            return _context.LeaveAllocations
               .Include(q => q.LeaveType)
               .FirstOrDefaultAsync(q => q.Id == id);
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
        {
            return _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();
        }

        public Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
        {
            return _context.LeaveAllocations
               .Where(q => q.EmployeeId == userId)
               .Include(q => q.LeaveType)
               .ToListAsync();
        }

        public Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return _context.LeaveAllocations
               .Include(q => q.LeaveType)
               .FirstOrDefaultAsync(q => q.EmployeeId == userId && q.LeaveTypeId == leaveTypeId);
        }
    }
}
