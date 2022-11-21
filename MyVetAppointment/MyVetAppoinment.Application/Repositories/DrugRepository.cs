using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class DrugRepository : EntityRepository<Drug>
    {
        private readonly Context databaseContext;
        public DrugRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Drug entity)
        {
            this.databaseContext.Drugs.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(Drug entity)
        {
            this.databaseContext.Drugs.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<Drug> GetAll()
        {
            return this.databaseContext.Drugs.ToList();
        }

        public Drug? GetById(Guid id)
        {
            return this.databaseContext.Drugs.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Drug entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
