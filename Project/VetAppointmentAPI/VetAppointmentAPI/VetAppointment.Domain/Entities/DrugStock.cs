using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class DrugStock
    {
        public DrugStock(Drug type, int quantity, int pricePerItem) : this(type.DrugId, quantity, pricePerItem)
        {
            Type = type;
        }

        private DrugStock(Guid typeId, int quantity, int pricePerItem)
        {
            DrugStockId = Guid.NewGuid();
            TypeId = typeId;
            Quantity = quantity;
            PricePerItem = pricePerItem;
        }

        public Guid DrugStockId { get; private set; }
        public Guid TypeId { get; private set; }
        public Drug? Type { get; private set; }
        public int Quantity { get; private set; }
        public int PricePerItem { get; private set; }
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

        public Result UpdatePricePerItem(int pricePerItem)
        {
            if (pricePerItem < 1)
                return Result.Failure("Price cannot be less than 1");
            PricePerItem = pricePerItem;
            return Result.Success();
        }
    }
}
