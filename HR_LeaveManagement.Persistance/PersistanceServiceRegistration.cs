using Microsoft.Extensions.DependencyInjection;

namespace HR_LeaveManagement.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            return services;
        }
    }
}