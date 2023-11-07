using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Queries
{
    public class GetLeaveAllocationListHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveAllocationListHandler>> _mockAppLogger;

        public GetLeaveAllocationListHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
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
            var handler = new GetLeaveAllocationListHandler(_mapper, _mockRepo.Object);
            var result = await handler.Handle(new GetLeaveAllocationListQuery(), CancellationToken.None);
            result.ShouldBeOfType<List<LeaveAllocationDto>>();
            result.Count.ShouldBe(3);
        }
    }
}
