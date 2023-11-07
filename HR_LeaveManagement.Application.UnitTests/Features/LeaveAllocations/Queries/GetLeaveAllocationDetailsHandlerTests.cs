using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetAllLeaveAllocation;
using HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveAllocations.Queries
{
    public class GetLeaveAllocationDetailsHandlerTests
    {
        private readonly Mock<ILeaveAllocationRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveAllocationDetailsHandler>> _mockAppLogger;

        public GetLeaveAllocationDetailsHandlerTests()
        {
            _mockRepo = MockLeaveAllocationRepository.GetMockLeaveAllocationRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveAllocationProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetLeaveAllocationDetailsHandler>>();
        }

        [Fact]
        public async Task GetLeaveAllocationDetails_Success()
        {
            var handler = new GetLeaveAllocationDetailsHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new GetLeaveAllocationDetailsQuery() { Id = 1}, CancellationToken.None);
            result.ShouldBeOfType<LeaveAllocationDetailsDto>();
        }

        [Fact]
        public void GetLeaveAllocationDetails_NotFound()
        {

            var handler = new GetLeaveAllocationDetailsHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<NotFoundException>(async () => await handler.Handle(new GetLeaveAllocationDetailsQuery() { Id = 4 }, CancellationToken.None))
                .Message.ShouldBe("leaveAllocationDetails (HR_LeaveManagement.Application.Contracts.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails.GetLeaveAllocationDetailsQuery)"); ;
        }
    }
}
