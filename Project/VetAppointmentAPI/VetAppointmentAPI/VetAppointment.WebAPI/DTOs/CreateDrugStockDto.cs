namespace VetAppointment.WebAPI.DTOs
{
    public class CreateDrugStockDto
    {
        public Guid TypeId { get; set; }
        public int Quantity { get; set; }
    }
}
