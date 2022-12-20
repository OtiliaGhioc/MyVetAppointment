using VetAppointment.Application.Repositories.Base;
using VetAppointment.Domain.Entities;

namespace VetAppointment.Application.Repositories.Interfaces
{
    public interface IDrugRepository : IBaseRepository<Drug>
    {
        public Task<Drug> UpdateAsync(Drug drug);
        public Task<Drug> DeleteAsync(Drug drug);
    }
}
