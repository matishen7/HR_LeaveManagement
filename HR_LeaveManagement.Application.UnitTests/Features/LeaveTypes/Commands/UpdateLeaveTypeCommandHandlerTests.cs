using AutoMapper;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.CreateLeaveType;
using HR_LeaveManagement.Application.Contracts.Features.LeaveType.Commands.UpdateLeaveType;
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
    public class UpdateLeaveTypeCommandHandlerTests
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private readonly IMapper _mapper;
        private readonly Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _mockAppLogger;

        public UpdateLeaveTypeCommandHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task UpdateLeaveType_Success()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            var result = await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Id = 1,
                DefaultDays = 10,
                Name = Guid.NewGuid().ToString(),
            }, CancellationToken.None);
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public Task UpdateLeaveType_NotFound()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new  UpdateLeaveTypeCommand()
            {
                Id = 5,
                Name = Guid.NewGuid().ToString(),
                DefaultDays = 10,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
            return Task.CompletedTask;
        }

        [Fact]
        public Task UpdateLeaveType_Id_Empty()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Name = Guid.NewGuid().ToString(),
                DefaultDays = 10,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
            return Task.CompletedTask;
        }

        [Fact]
        public Task UpdateLeaveType_InvalidLeaveType_DefaultDays()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Name = Guid.NewGuid().ToString(),
                DefaultDays = 0,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
            return Task.CompletedTask;
        }

        [Fact]
        public Task UpdateLeaveType_InvalidLeaveType_DefaultDays_GreaterThan_100()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Name = Guid.NewGuid().ToString(),
                DefaultDays = 101,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
            return Task.CompletedTask;
        }

        [Fact]
        public Task UpdateLeaveType_InvalidLeaveType_Name_GreaterThan_70_Characters()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);
            Should.Throw<BadRequestException>(async () => await handler.Handle(new UpdateLeaveTypeCommand()
            {
                Name = "DJPuKtgGALZnXgJh04A1vakGFm7B2Vfnn1BgQa7dnbbf6bFiYKahzaAizBLcUV0dYDX3p8R",
                DefaultDays = 10,
            }, CancellationToken.None))
                .Message.ShouldBe("Invalid LeaveType"); ;
            return Task.CompletedTask;
        }
    }
}
