using VetAppointment.Application.Repositories.Base;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointment>
    {
    }
}
