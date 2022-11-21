using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class AppointmentRepository : EntityRepository<Appointment>
    {
        private readonly Context databaseContext;
        public AppointmentRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Appointment entity)
        {
            this.databaseContext.Appointments.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(Appointment entity)
        {
            this.databaseContext.Appointments.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return this.databaseContext.Appointments.ToList();
        }

        public Appointment? GetById(Guid id)
        {
            return this.databaseContext.Appointments.FirstOrDefault(c => c.Id == id);
        }

        public void Update(Appointment entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
