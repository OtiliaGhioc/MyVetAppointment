namespace VetAppointment.Domain.Entities
{
    public class Appointment
    {
        public Appointment(User appointer, User appointee, DateTime dateTime, string title, string description, string type) :
            this(appointer.UserId, appointee.UserId, dateTime, title, description, type)
        {
            Appointer = appointer;
            Appointee = appointee;
        }

        private Appointment(Guid appointerId, Guid appointeeId, DateTime dateTime, string title, string description, string type)
        {
            AppointmentId = Guid.NewGuid();
            AppointerId = appointerId;
            AppointeeId = appointeeId;
            DateTime = dateTime;
            Description = description;
            Title = title;
            Type = type;
        }
        public Guid AppointmentId { get; private set; }
        public Guid AppointerId { get; private set; }
        public User? Appointer { get; private set; }
        public Guid AppointeeId { get; private set; }
        public User? Appointee { get; private set; }
        public bool IsExpired { get; set; } = false;
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
}
