using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class OfficeRepository : EntityRepository<Office>
    {
        private readonly Context databaseContext;
        public OfficeRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Office entity)
        {
            this.databaseContext.Offices.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(Office entity)
        {
            this.databaseContext.Offices.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<Office> GetAll()
        {
            return this.databaseContext.Offices.ToList();
        }

        public Office? GetById(Guid id)
        {
            return this.databaseContext.Offices.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Office entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
