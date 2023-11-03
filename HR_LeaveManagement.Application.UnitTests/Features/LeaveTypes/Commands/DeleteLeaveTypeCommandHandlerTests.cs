using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.DeleteLeaveType;
using HR_LeaveManagement.Application.Contracts.Logging;
using HR_LeaveManagement.Application.Contracts.Persistence;
using HR_LeaveManagement.Application.Exceptions;
using HR_LeaveManagement.Application.MappingProfiles;
using HR_LeaveManagement.Application.UnitTests.Mocks;
using MediatR;
using Moq;
using Shouldly;

namespace HR_LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class DeleteLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<DeleteLeaveTypeCommandHandler>> _mockAppLogger;

        public DeleteLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<DeleteLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task DeleteLeaveType_Success()
        {
            var handler = new DeleteLeaveTypeCommandHandler(_mapper, _mockRepo.Object);
            var result = await handler.Handle(new DeleteLeaveTypeCommand()
            {
                Id = 1,
            }, CancellationToken.None);
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public Task DeleteLeaveType_NotFound()
        {
            var handler = new DeleteLeaveTypeCommandHandler(_mapper, _mockRepo.Object);
            Should.Throw<NotFoundException>(async () => await handler.Handle(new DeleteLeaveTypeCommand()
            {
                Id = 5,
            }, CancellationToken.None))
                .Message.ShouldBe("leaveTypeToDelete (HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.DeleteLeaveType.DeleteLeaveTypeCommand)"); ;
            return Task.CompletedTask;
        }
    }
}
