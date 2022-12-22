using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.DTOs
{
    public class DrugStockDto : CreateDrugStockDto
    {
        public Guid Id { get; set; }
        public Drug? Type { get; set; }
    }

    public class DrugStockDetailDto
    {
        public DrugStockDetailDto(DrugStock drugStock, Drug drug)
        {
            DrugStockId = drugStock.DrugStockId;
            Quantity = drugStock.Quantity;
            TypeId = drug.DrugId;
            DrugName = drug.Title;
            PricePerItem = drugStock.PricePerItem;
        }
        public Guid DrugStockId { get; set; }
        public Guid TypeId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
        public int PricePerItem { get; set; }
    }
}
