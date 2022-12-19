using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Impl
{
    public class BillingEntryRepository : BaseRepository<BillingEntry>, IBillingEntryRepository
    {
        public BillingEntryRepository(IDatabaseContext context) : base(context) { }
        
    }
}
