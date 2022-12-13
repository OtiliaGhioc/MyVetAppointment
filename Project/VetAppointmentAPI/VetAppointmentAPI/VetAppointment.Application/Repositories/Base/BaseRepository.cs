using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
            return entity;
        }

        public virtual async Task Delete(T entity)
        {
            EntityEntry entry = context.Set<T>().Entry(entity);
            entry.State = EntityState.Deleted;
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

        public virtual async Task Update(T entity)
        {
            EntityEntry entry = context.Set<T>().Entry(entity);
            entry.State = EntityState.Modified;
        }

        public async Task SaveChanges()
        {
            await context.Save();
        }
    }
}
