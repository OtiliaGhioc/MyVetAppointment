using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Impl
{
    public class BillingEntryRepository : BaseRepository<BillingEntry>, IBillingEntryRepository
    {
        public BillingEntryRepository(IDatabaseContext context) : base(context) { }
        private readonly IDatabaseContext context;

        public void Add(BillingEntry bill)
        {
            context.BillingEntries.Add(bill);
        }
        public void Update(BillingEntry bill)
        {
            context.BillingEntries.Update(bill);
        }
        public void Delete(BillingEntry bill)
        {
            context.BillingEntries.Remove(bill);
        }
        public IEnumerable<BillingEntry> GetAll()
        {
            return context.BillingEntries.ToList();
        }

        public BillingEntry GetById(Guid id)
        {
            return context.BillingEntries.Find(id);
        }

        public void Save()
        {
            context.Save();
        }
    }
}
