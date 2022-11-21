namespace MyVetAppointment.Domain.Entities
{
    public class Drug
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public float Price { get; private set; }
        public Drug(string title, float price)
        {
            Id = Guid.NewGuid();
            Title = title;
            Price = price;
        }
    }
}
