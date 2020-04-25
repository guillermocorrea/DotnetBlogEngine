using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAsyncBaseRepository<T, Key>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Key id);
        Task CreateAsync(T entity);
        Task UpdateAsync(Key id, T entity);
        Task RemoveAsync(Key id);
    }
}
