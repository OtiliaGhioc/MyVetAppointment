namespace MyVetAppoinment.Application.Repositories
{
    public interface EntityRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetById(Guid id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
