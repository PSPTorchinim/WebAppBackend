using Handlers.Entities;
using System.Linq.Expressions;

namespace Handlers.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAsync();
        Task<T> GetAsync(Guid id);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> specification);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<T> Delete(Guid id);
        T Delete(T entity);
        Task<bool> Save();
        Task<bool> Empty();
        Task<int> CountAsync(Expression<Func<T, bool>> specification);
    }
}
