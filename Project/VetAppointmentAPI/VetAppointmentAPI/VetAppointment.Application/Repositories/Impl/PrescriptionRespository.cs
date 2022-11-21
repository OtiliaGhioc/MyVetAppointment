using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Impl
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(IDatabaseContext context) : base(context) { }
    }
}
