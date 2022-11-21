namespace VetAppointment.Domain.Entities
{
    public class Drug
    {
        public Drug(string title, int price)
        {
            DrugId = Guid.NewGuid();
            Title = title;
            Price = price;
        }

        public Guid DrugId { get; private set; }
        public string Title { get; private set; }
        public int Price { get; private set; }
    }
}
