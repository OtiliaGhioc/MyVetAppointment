using VetAppointment.Application.Repositories.Interfaces;
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

        public class MedicalEntryEssentialOnlyDto
        {
            public MedicalEntryEssentialOnlyDto(MedicalHistoryEntry medicalHistoryEntry, User appointer)
            {
                MedicalHistoryEntryId = medicalHistoryEntry.MedicalHistoryEntryId;
                Appointer = appointer.Username;
                Title = medicalHistoryEntry.Appointment != null ? medicalHistoryEntry.Appointment.Title : "No title";
                Date = medicalHistoryEntry.Appointment != null ? medicalHistoryEntry.Appointment.DueDate.ToString("dd-MMM-yyyy") : "No date";
            }

            public Guid MedicalHistoryEntryId { get; private set; }
            public string Appointer { get; private set; }
            public string Title { get; private set; }
            public string Date { get; private set; }
        }

        public class MedicalEntryEssentialOnlyDtoWithPrescription
        {
            public MedicalEntryEssentialOnlyDtoWithPrescription(MedicalHistoryEntry medicalHistoryEntry, string title)
            {
                Title = title;
                PrescriptionId = medicalHistoryEntry.PrescriptionId;
                AppointmentId = medicalHistoryEntry.AppointmentId;
            }

            public Guid MedicalHistoryEntryId { get; private set; }
            public string Title { get; private set; }
            public Guid PrescriptionId { get; private set; }
            public Guid AppointmentId { get; private set; }
        }
    }
}
