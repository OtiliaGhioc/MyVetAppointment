using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class MedicalHistoryRepository : EntityRepository<MedicalHistory>
    {
        private readonly Context databaseContext;
        public MedicalHistoryRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(MedicalHistory entity)
        {
            this.databaseContext.MedicalHistories.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(MedicalHistory entity)
        {
            this.databaseContext.MedicalHistories.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<MedicalHistory> GetAll()
        {
            return this.databaseContext.MedicalHistories.ToList();
        }

        public MedicalHistory? GetById(Guid id)
        {
            return this.databaseContext.MedicalHistories.FirstOrDefault(c => c.Id == id);
        }

        public void Update(MedicalHistory entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
