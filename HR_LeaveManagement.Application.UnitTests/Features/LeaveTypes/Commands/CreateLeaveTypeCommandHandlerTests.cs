using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<CreateLeaveTypeCommandHandler>> _mockAppLogger;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<CreateLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task CreateLeaveType_Success()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new CreateLeaveTypeCommand()
            {
                DefaultDays = 10,
                Name = Guid.NewGuid().ToString(),
            }, CancellationToken.None);

            result.ShouldBeOfType<int>();
        }

        [Fact]
        public async Task CreateLeaveType_IsLeaveTypeUniqueName_False()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new CreateLeaveTypeCommand()
            {
                Name = "Non Unique Name",
                DefaultDays = 10,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
        }
    }
}
