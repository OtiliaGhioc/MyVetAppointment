using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VetAppointment.Infrastructure.Context;

namespace VetAppointment.Application.Repositories.Base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IDatabaseContext context;

        protected BaseRepository(IDatabaseContext context)
        {
            this.context = context;
        }

        public virtual async Task<T> Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.Save();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>()
                .AsQueryable()
                .Where(predicate).ToListAsync();
        }

        public virtual async Task<T?> Get(Guid id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> All()
        {
            return await context.Set<T>()
                .ToListAsync();
        }

        public virtual void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async Task SaveChanges()
        {
            await context.Save();
        }

        public async Task DeleteAsync(T entity)
        {
            context.Remove(entity);
            await context.Save();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
            await context.Save();
            return entity;
        }
    }
}
