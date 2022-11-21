namespace VetAppointment.Domain.Entities
{
    public class User
    {
        public User(string username, string password, bool hasOffice)
        {
            UserId = Guid.NewGuid();
            Username = username;
            Password = CalculatePasswordHash(password);
            HasOffice = hasOffice;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool HasOffice { get; private set; }
        public Office? UserOffice { get; set; }

        private string CalculatePasswordHash(string password)
        {
            return password;
        }
    }
}
