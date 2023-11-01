using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeDetailsHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveTypeDetailsHandler>> _mockAppLogger;

        public GetLeaveTypeDetailsHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetLeaveTypeDetailsHandler>>();
        }

        [Fact]
        public async Task GetLeaveTypeDetailsTest()
        {
            var handler = new GetLeaveTypeDetailsHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);
            result.ShouldBeOfType<LeaveTypeDetailsDto>();
            result.Id.ShouldBe(1);
        }
    }
}
