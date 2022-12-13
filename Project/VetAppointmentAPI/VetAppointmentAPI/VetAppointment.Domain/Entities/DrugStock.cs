using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class DrugStock
    {
        public DrugStock(Drug type, int quantity) : this(type.DrugId, quantity)
        {
            Type = type;
        }

        private DrugStock(Guid typeId, int quantity)
        {
            DrugStockId = Guid.NewGuid();
            TypeId = typeId;
            Quantity = quantity;
        }

        public Guid DrugStockId { get; private set; }
        public Guid TypeId { get; private set; }
        public Drug? Type { get; private set; }
        public int Quantity { get; private set; }

        public Result RemoveDrugsFromPublicStock(int quantity)
        {
            if (quantity > Quantity)
                return Result.Failure("Not enough stock!");
            if (quantity < 0)
                return Result.Failure("Cannot add negative stock!");
            Quantity -= quantity;
            return Result.Success();
        }

        public Result AddDrugsInStock(int quantity)
        {
            if (quantity < 0)
                return Result.Failure("Cannot add negative stock!");
            Quantity += quantity;
            return Result.Success();
        }
    }
}
