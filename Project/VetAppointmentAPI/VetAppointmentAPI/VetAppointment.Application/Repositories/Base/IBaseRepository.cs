using System.Linq.Expressions;

namespace VetAppointment.Application.Repositories.Base
{
    public interface IBaseRepository<T>
    {
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> All();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task SaveChanges();
    }
}
