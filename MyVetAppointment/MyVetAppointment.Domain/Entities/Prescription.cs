namespace MyVetAppointment.Domain.Entities
{
    public class Prescription
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }
        public List<Drug> Drugs { get; private set; }
        public Prescription(string description)
        {
            Id = Guid.NewGuid();
            Description = description;
            Drugs = new List<Drug>();
        }
    }
}
