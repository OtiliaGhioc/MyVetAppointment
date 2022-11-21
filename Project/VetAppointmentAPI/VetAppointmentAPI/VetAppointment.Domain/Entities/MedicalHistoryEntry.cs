namespace VetAppointment.Domain.Entities
{
    public class MedicalHistoryEntry
    {
        public MedicalHistoryEntry(Appointment appointment, Prescription prescription) :
            this(appointment.AppointmentId, prescription.PrescriptionId)
        {
            Appointment = appointment;
            Prescription = prescription;
        }

        private MedicalHistoryEntry(Guid appointmentId, Guid prescriptionId)
        {
            MedicalHistoryEntryId = Guid.NewGuid();
            AppointmentId = appointmentId;
            PrescriptionId = prescriptionId;
        }

        public Guid MedicalHistoryEntryId { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Appointment? Appointment { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public Prescription? Prescription { get; private set; }
    }
}
