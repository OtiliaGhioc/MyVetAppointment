namespace VetAppointment.WebAPI.DTOs
{
    public class CreateDrugStockDto
    {
        public Guid TypeId { get; set; }
        public int Quantity { get; set; }
        public int PricePerItem { get; set; }
    }

    public class UpdateDrugStockDto 
    {
        public int? SupplyQuantity { get; set; }
        public int? PricePerItem { get; set; }

    }
}
