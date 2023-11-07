using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Queries
{
    public class GetLeaveAllocationListHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveAllocationListHandler>> _mockAppLogger;

        public GetLeaveTypeDetailsHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveAllocationProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetLeaveAllocationListHandler>>();
        }

        [Fact]
        public async Task GetLeaveAllocationList_Success()
        {
            var handler = new GetLeaveTypeDetailsHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);
            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            result.Id.ShouldBe(1);
        }
    }
}
