using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.DTOs
{
    public class PrescriptionDto : CreatePrescriptionDto
    {
        public Guid Id { get; set; }
        public List<PrescriptionDrug> Drugs { get; set; }
    }
}
