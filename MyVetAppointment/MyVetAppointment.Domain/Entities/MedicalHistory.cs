namespace MyVetAppointment.Domain.Entities
{
    public class MedicalHistory
    {
        public Guid Id { get; private set; }
        public Appointment Appointment { get; private set; }
        public Prescription Prescription { get; private set; }
        public MedicalHistory(Appointment appointment, Prescription prescription)
        {
            Id = Guid.NewGuid();
            Appointment = appointment;
            Prescription = prescription;
        }
    }
}
