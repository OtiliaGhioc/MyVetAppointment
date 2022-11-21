namespace VetAppointment.Domain.Entities
{
    public class BillingEntry
    {
        public BillingEntry(User issuer, User customer, DateTime dateTime, Prescription prescription, Appointment appointment, int price) :
            this(issuer.UserId, customer.UserId, dateTime, prescription.PrescriptionId, appointment.AppointmentId, price)
        {
            Issuer = issuer;
            Customer = customer;
            Prescription = prescription;
            Appointment = appointment;
        }

        private BillingEntry(Guid issuerId, Guid customerId, DateTime dateTime, Guid prescriptionId, Guid appointmentId, int price)
        {
            BillingEntryId = Guid.NewGuid();
            IssuerId = issuerId;
            CustomerId = customerId;
            DateTime = dateTime;
            PrescriptionId = prescriptionId;
            AppointmentId = appointmentId;
            Price = price;
        }

        public Guid BillingEntryId { get; private set; }
        public Guid IssuerId { get; private set; }
        public User? Issuer { get; private set; }
        public Guid CustomerId { get; private set; }
        public User? Customer { get; private set; }
        public DateTime DateTime { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public Prescription? Prescription { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Appointment? Appointment { get; private set; }
        public int Price { get; private set; }

    }
}
