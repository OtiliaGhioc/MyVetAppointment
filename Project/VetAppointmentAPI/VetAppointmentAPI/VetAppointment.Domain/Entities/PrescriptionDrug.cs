using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class PrescriptionDrug
    {
        private PrescriptionDrug(DrugStock stock, int quantity) : this(stock.DrugStockId, quantity)
        {
            Stock = stock;
        }

        private PrescriptionDrug(Guid stockId, int quantity)
        {
            PrescriptionDrugId = Guid.NewGuid();
            StockId = stockId;
            Quantity = quantity;
        }

        public Guid PrescriptionDrugId { get; private set; }
        public Guid StockId { get; private set; }
        public DrugStock? Stock { get; private set; }
        public int Quantity { get; private set; }

        public static Result<PrescriptionDrug> CreatePrescriptionDrug(DrugStock stock, int quantity)
        {
            Result removeStock = stock.RemoveDrugsFromPublicStock(quantity);
            if (removeStock.IsFailure)
                return Result<PrescriptionDrug>.Failure(removeStock.Error);
            
            PrescriptionDrug instance = new PrescriptionDrug(stock, quantity);
            return Result<PrescriptionDrug>.Success(instance);
        }
    }
}
