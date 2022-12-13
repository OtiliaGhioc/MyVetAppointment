using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.DTOs
{
    public class DrugStockDto : CreateDrugStockDto
    {
        public Guid Id { get; set; }
        public required Drug Type { get; set; }
    }
}
