namespace MyVetAppointment.Domain.Entities
{
    public class User
    {
        public Guid Id { get;  private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool HasOffice { get; private set; }
        public Office? UserOffice { get; set; }

        public User(string username, string password, bool hasOffice)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            HasOffice = hasOffice;
        }
    }
}
