using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class PrescriptionRepository : EntityRepository<Prescription>
    {
        private readonly Context databaseContext;
        public PrescriptionRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Prescription entity)
        {
            this.databaseContext.Prescriptions.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(Prescription entity)
        {
            this.databaseContext.Prescriptions.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<Prescription> GetAll()
        {
            return this.databaseContext.Prescriptions.ToList();
        }

        public Prescription? GetById(Guid id)
        {
            return this.databaseContext.Prescriptions.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Prescription entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
