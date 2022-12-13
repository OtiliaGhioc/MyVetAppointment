namespace VetAppointment.WebAPI.Dtos
{
    public class OfficeDto
    {
        public Guid OfficeId { get; set; }
        public required string Address { get; set; }
    }
}
