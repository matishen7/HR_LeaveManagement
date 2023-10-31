using HR_LeaveManagement.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;

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
        public void Save()
        {

        }
    }
}