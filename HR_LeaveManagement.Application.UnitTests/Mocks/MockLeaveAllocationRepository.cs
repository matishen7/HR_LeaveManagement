using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;
using Moq;

namespace HR_LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveAllocationRepository
    {
        public static Mock<ILeaveAllocationRepository> GetMockLeaveAllocationRepository()
        {
            var leaveAllocations = new List<LeaveAllocation>()
            {
                new LeaveAllocation
                {
                   Id = 1,
                   DateCreated = DateTime.Now,
                   DateModified = DateTime.Now,
                   EmployeeId = "1",
                   LeaveTypeId = 1,
                   NumberOfDays = 1,
                   Period = 1,
                },
                new LeaveAllocation
                {
                   Id = 2,
                   DateCreated = DateTime.Now,
                   DateModified = DateTime.Now,
                   EmployeeId = "2",
                   LeaveTypeId = 2,
                   NumberOfDays = 2,
                   Period = 2,
                },
                new LeaveAllocation
                {
                   Id = 3,
                   DateCreated = DateTime.Now,
                   DateModified = DateTime.Now,
                   EmployeeId = "3",
                   LeaveTypeId = 3,
                   NumberOfDays = 3,
                   Period = 3,
                },
            };


            var mockRepo = new Mock<ILeaveAllocationRepository>();
            mockRepo.Setup(r => r.GetLeaveAllocationWithDetails()).ReturnsAsync(leaveAllocations);
            mockRepo.Setup(r => r.GetLeaveAllocationWithDetails(It.IsAny<string>())).Returns((string userId) =>
            {
                var leaveAllocation = leaveAllocations.FindAll(t => t.EmployeeId == userId);
                return Task.FromResult(leaveAllocation);
            });
            mockRepo.Setup(r => r.GetLeaveAllocationWithDetails(It.IsAny<int>())).Returns((int id) =>
            {
                var leaveAllocation = leaveAllocations.SingleOrDefault(t => t.Id == id);
                return Task.FromResult(leaveAllocation);
            });
            return mockRepo;
        }
    }
}
