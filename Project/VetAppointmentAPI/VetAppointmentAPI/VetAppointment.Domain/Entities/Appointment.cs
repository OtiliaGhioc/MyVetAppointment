namespace VetAppointment.Domain.Entities
{
    public class Appointment
    {
        public Appointment(User appointer, User appointee, DateTime dateTime, string description, string type) :
            this(appointer.UserId, appointee.UserId, dateTime, description, type)
        {
            Appointer = appointer;
            Appointee = appointee;
        }

        private Appointment(Guid appointerId, Guid appointeeId, DateTime dateTime, string description, string type)
        {
            AppointmentId = Guid.NewGuid();
            AppointerId = appointerId;
            AppointeeId = appointeeId;
            DateTime = dateTime;
            Description = description;
            Type = type;
        }
        public Guid AppointmentId { get; private set; }
        public Guid AppointerId { get; private set; }
        public User? Appointer { get; private set; }
        public Guid AppointeeId { get; private set; }
        public User? Appointee { get; private set; }
        public bool isExpired { get; private set; } = false;
        public DateTime DateTime { get; private set; }
        public string Description { get; private set; }
        public string Type { get; private set; }
    }
}
