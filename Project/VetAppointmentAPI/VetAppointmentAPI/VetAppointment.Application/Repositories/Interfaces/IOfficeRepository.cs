using VetAppointment.Application.Repositories.Base;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Interfaces
{
    public interface IOfficeRepository : IBaseRepository<Office>
    {
        public Task<Office> UpdateAsync(Office office);
    }
}
