using HR_LeaveManagement.Domain;

namespace HR_LeaveManagement.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> GetByIdAsync(int id);
    }
}