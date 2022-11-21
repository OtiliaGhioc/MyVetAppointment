namespace MyVetAppointment.Domain.Entities
{
    public class DrugStock
    {
        public Guid Id { get; private set; }
        public Drug Type { get; private set; }
        public int Quantity { get; private set; }
        public DrugStock(Drug type, int quantity)
        {
            Id = Guid.NewGuid();
            Type = type;
            Quantity = quantity;
        }
    }
}
