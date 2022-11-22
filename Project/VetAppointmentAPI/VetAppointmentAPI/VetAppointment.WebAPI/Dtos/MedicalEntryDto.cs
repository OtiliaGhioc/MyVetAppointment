using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.Dtos
{
    namespace MedicalEntryDto 
    {
        public class MedicalEntryCreateDto
        {
            public MedicalEntryCreateDto(Guid appointmentId, Guid prescriptionId)
            {
                AppointmentId = appointmentId;
                PrescriptionId = prescriptionId;
            }

            public Guid AppointmentId { get; set; }
            public Guid PrescriptionId { get; set; }
        }

        public class MedicalEntryDetailDto
        {
            public MedicalEntryDetailDto(MedicalHistoryEntry medicalHistoryEntry)
            {
                AppointmentId = medicalHistoryEntry.AppointmentId;
                PrescriptionId = medicalHistoryEntry.PrescriptionId;
            }

            public Guid AppointmentId { get; set; }
            public Guid PrescriptionId { get; set; }
        }
    }
}
