using VetAppointment.Application.Repositories.Base;
using VetAppointment.Application.Repositories.Interfaces;
using VetAppointment.Domain.Entities;
using VetAppointment.Infrastructure.Context;

namespace VetAppointment.Application.Repositories.Impl
{
    public class DrugStockRepository : BaseRepository<DrugStock>, IDrugStockRepository
    {
        public DrugStockRepository(IDatabaseContext context) : base(context) { }
    }
}
