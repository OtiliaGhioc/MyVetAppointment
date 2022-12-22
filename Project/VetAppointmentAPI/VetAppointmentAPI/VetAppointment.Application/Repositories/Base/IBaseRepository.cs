using System.Linq.Expressions;

namespace VetAppointment.Application.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        Task<T> Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
