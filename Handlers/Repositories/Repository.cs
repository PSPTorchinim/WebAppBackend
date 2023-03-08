using Handlers.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Handlers.Repositories
{
    public abstract class Repository<T, TContext> : IRepository<T> where T : class, IGuidEntity where TContext : DbContext
    {

        protected TContext context;

        public Repository(TContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> Add(T entity)
        {
            context.Set<T>().Add(entity);
            return await Save();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> specification)
        {
            var matching = await GetAsync(specification);
            return matching.Count();
        }

        public async Task<T> Delete(Guid id)
        {
            var x = await GetAsync(id);
            return Delete(x);
        }

        public T Delete(T entity)
        {
            var x = context.Set<T>().Remove(entity);
            return x.Entity;
        }

        public async Task<bool> Empty()
        {
            var any = context.Set<T>().Count() > 0;
            return !any;
        }

        public async Task<List<T>> GetAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetAsync(Expression<Func<T, bool>> specification)
        {
            return await context.Set<T>().Where(specification).ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            var updated = await context.SaveChangesAsync() > 0;
            return updated;
        }
    }
}
