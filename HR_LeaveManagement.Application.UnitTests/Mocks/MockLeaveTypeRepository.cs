using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Domain;
using Moq;

namespace HR_LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>()
            {
                new LeaveType
                {
                    Id = 1,
                    DefaultDays = 1,
                    Name = "Sick",
                },
                new LeaveType
                {
                    Id = 2,
                    DefaultDays = 10,
                    Name = "Paternity",
                },
                new LeaveType
                {
                    Id = 3,
                    DefaultDays = 15,
                    Name = "Maternity",
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);

            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                .Returns((LeaveTypeDetailsDto leaveType) =>
                {
                    var  lt = leaveTypes.SingleOrDefault(l => l.Id == leaveType.Id);
                    return lt;
                });
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });
            return mockRepo;
        }
    }
}
