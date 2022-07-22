using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task Delete(T entity);
        Task Update(T entity);
        Task<T> Add(T entity);
        Task<bool> Exists(int id);
    }
}