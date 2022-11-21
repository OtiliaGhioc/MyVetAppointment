using VetAppointment.Application.Repositories.Base;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Interfaces
{
    public interface IBillingEntryRepository : IBaseRepository<BillingEntry>
    {
        void Add(BillingEntry billingEntry);
        void Delete(BillingEntry billingEntry);
        IEnumerable<BillingEntry> GetAll();
        BillingEntry GetById(Guid id);
        void Save();
        void Update(BillingEntry billingEntry);
    }
}
