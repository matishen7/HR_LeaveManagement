using HR_LeaveManagement.Domain;
using HR_LeaveManagement.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR_LeaveManagement.Persistence.IntegrationTests
{
    public class HrDatabaseContextTests
    {
        private readonly HrDatabaseContext _hrDbCOntext;

        public HrDatabaseContextTests()
        {
            var _dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            _hrDbCOntext = new HrDatabaseContext(_dbOptions);
        }
        [Fact]
        public async Task Save_SetDateCreatedValueAsync()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 1,
                Name = "Sick",
            };

            //Act

            await _hrDbCOntext.LeaveTypes.AddAsync(leaveType);
            await _hrDbCOntext.SaveChangesAsync();
            //Assert

            leaveType.DateCreated.ShouldNotBeNull();
        }

        [Fact]
        public async void Save_SetDateMOdifiedValue()
        {
            //Arrange
            var leaveType = new LeaveType
            {
                Id = 1,
                DefaultDays = 1,
                Name = "Sick",
            };

            //Act

            await _hrDbCOntext.LeaveTypes.AddAsync(leaveType);
            await _hrDbCOntext.SaveChangesAsync();
            //Assert

            leaveType.DateModified.ShouldNotBeNull();
        }
    }
}