using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class BillingEntryRepository : EntityRepository<BilingEntry>
    {
        private readonly Context databaseContext;
        public BillingEntryRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(BilingEntry entity)
        {
            this.databaseContext.BilingEntries.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(BilingEntry entity)
        {
            this.databaseContext.BilingEntries.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<BilingEntry> GetAll()
        {
            return this.databaseContext.BilingEntries.ToList();
        }

        public BilingEntry? GetById(Guid id)
        {
            return this.databaseContext.BilingEntries.FirstOrDefault(c => c.Id == id);
        }

        public void Update(BilingEntry entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
