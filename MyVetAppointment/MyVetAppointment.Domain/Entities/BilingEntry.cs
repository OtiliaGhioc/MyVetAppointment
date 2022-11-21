namespace MyVetAppointment.Domain.Entities
{
    public class BilingEntry
    {
        public Guid Id { get; private set; }
        public string Issuer { get; private set; }
        public string Customer { get; private set; }
        public DateTime DateTime { get; private set; }
        public Prescription? Prescription { get; private set; }
        public Appointment? Appointment { get; private set; }
        public float Price { get; private set; }
        public BilingEntry(string issuer, string customer, DateTime dateTime, Prescription? prescription, Appointment appointment, float price)
        {
            Id = Guid.NewGuid();
            Issuer = issuer;
            Customer = customer;
            DateTime = dateTime;
            Prescription = prescription;
            Appointment = appointment;
            Price = price;
        }
    }
}
