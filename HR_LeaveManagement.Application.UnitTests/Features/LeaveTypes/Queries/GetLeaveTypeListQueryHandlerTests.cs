using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries
{
    public class GetLeaveTypeListQueryHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<GetLeaveTypeListQueryHandler>> _appLogger;

        public GetLeaveTypeListQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _appLogger = new Mock<IAppLogger<GetLeaveTypeListQueryHandler>>();
        }
    }
}
