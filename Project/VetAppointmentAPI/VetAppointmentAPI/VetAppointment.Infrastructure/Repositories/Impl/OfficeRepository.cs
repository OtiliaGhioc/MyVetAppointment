using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Infrastructure.Context;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Impl
{
    public class OfficeRepository : BaseRepository<Office>, IOfficeRepository
    {
        public OfficeRepository(IDatabaseContext context) : base(context) { }

        public async Task<Office> UpdateAsync(Office office)
        {
            context.Set<Office>().Update(office);
            return office;
        }
    }
}
