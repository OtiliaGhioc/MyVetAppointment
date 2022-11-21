namespace MyVetAppointment.Domain.Entities
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public User Appointer { get; private set; }
        public User Appointee { get; private set; }
        public bool IsExpired { get; private set; }
        public DateTime DateTime { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
        public Appointment(User appointer, User appointee, bool isExpired, DateTime dateTime, string description, string type)
        {
            Id = Guid.NewGuid();
            Appointer = appointer;
            Appointee = appointee;
            IsExpired = isExpired;
            DateTime = dateTime;
            Description = description;
            Type = type;
        }
    }
}
