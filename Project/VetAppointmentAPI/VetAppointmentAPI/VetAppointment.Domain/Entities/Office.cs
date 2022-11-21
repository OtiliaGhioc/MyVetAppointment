namespace VetAppointment.Domain.Entities
{
    public class Office
    {
        public Office(string address)
        {
            OfficeId = Guid.NewGuid();
            Address = address;
        }
        public Guid OfficeId { get; private set; }
        public string Address { get; private set; }
    }
}
