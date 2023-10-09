using HR_LeaveManagement.Persistance.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR_LeaveManagement.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HrDatabaseContext>(options => { 
                options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
            });
            return services;
        }
    }
}