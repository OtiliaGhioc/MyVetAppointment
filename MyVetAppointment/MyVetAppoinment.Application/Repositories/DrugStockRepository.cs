using MyVetAppointment.Domain.Entities;
using MyVetAppointment.Infrastructure;

namespace MyVetAppoinment.Application.Repositories
{
    public class DrugStockRepository : EntityRepository<DrugStock>
    {
        private readonly Context databaseContext;
        public DrugStockRepository(Context databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public void Add(DrugStock entity)
        {
            this.databaseContext.DrugStocks.Add(entity);
            this.databaseContext.SaveChanges();
        }

        public void Delete(DrugStock entity)
        {
            this.databaseContext.DrugStocks.Remove(entity);
            this.databaseContext.SaveChanges();
        }

        public IEnumerable<DrugStock> GetAll()
        {
            return this.databaseContext.DrugStocks.ToList();
        }

        public DrugStock? GetById(Guid id)
        {
            return this.databaseContext.DrugStocks.FirstOrDefault(c => c.Id == id);
        }

        public void Update(DrugStock entity)
        {
            this.databaseContext.Update(entity);
            this.databaseContext.SaveChanges();
        }
    }
}
