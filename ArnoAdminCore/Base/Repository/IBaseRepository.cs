using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.Base.Repository
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> FindAllAsync();
        Task<T> FindByIdAsync(long id);
        Task<IEnumerable<T>> FindByIdsAsync(IEnumerable<long> ids);
        T Add(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(T entity);
        Task<bool> ExistsAsync(long id);
        Task<bool> SaveAsync();
    }
}
