using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class UserRepository : EntityRepository<User>
    {
        private readonly Context databaseContext;
        public UserRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(User entity)
        {
            this.databaseContext.Users.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(User entity)
        {
            this.databaseContext.Users.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return this.databaseContext.Users.ToList();
        }

        public User? GetById(Guid id)
        {
            return this.databaseContext.Users.FirstOrDefault(c => c.Id == id);
        }

        public void Update(User entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
