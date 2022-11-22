using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.DTOs
{
    public class BillingEntryDto
    {
        public Guid BillingEntryId { get;  set; }
        public Guid IssuerId { get;  set; }
        public Guid CustomerId { get;  set; }
        public DateTime DateTime { get;  set; }
        public Guid PrescriptionId { get;  set; }
        public Guid AppointmentId { get;  set; }
        public int Price { get;  set; }
    }
}
