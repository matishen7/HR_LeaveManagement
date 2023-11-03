﻿using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
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
                 .Returns((int id) =>
                 {
                     var leaveType = leaveTypes.SingleOrDefault(t => t.Id == id);
                     return Task.FromResult(leaveType);
                 });
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveType>()))
               .Returns((LeaveType leaveType) =>
               {
                   var updatedLeaveType = leaveTypes.SingleOrDefault(l => l.Id == leaveType.Id);
                   updatedLeaveType.DefaultDays = leaveType.DefaultDays;
                   updatedLeaveType.Name = leaveType.Name;
                   return Task.CompletedTask;
               });

            mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>()))
            .ReturnsAsync(true);

            mockRepo.Setup(r => r.IsLeaveTypeUnique("Non Unique Name"))
           .ReturnsAsync(false);
            return mockRepo;
        }
    }
}
