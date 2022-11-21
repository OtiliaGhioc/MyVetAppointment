namespace MyVetAppointment.Domain.Entities
{
    public class Office
    {
        public Guid Id { get; private set; }
        public string Address { get; private set; }
        public Office(string address)
        {
            Id = Guid.NewGuid();
            Address = address;
        }
    }
}
