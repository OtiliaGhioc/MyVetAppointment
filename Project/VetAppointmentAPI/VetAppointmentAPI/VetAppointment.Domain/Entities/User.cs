using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class User
    {
        public User(string username, string password)
        {
            UserId = Guid.NewGuid();
            Username = username;
            Password = CalculatePasswordHash(password);
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool HasOffice { get; private set; } = false;
        public Guid OfficeId { get; private set; }
        public Office? UserOffice { get; set; }

        public Result RegisterOfficeToUser(Office office)
        {
            if (office == null)
                return Result.Failure("Cannot register null object as office!");
            UserOffice = office;
            OfficeId = office.OfficeId;
            HasOffice = true;
            return Result.Success();
        }

        public Result UnregisterOfficeFromUser()
        {
            if (UserOffice == null)
                return Result.Failure("User does not have an office registered!");
            UserOffice = null;
            OfficeId = Guid.Empty;
            HasOffice = false;
            return Result.Success();
        }

        private string CalculatePasswordHash(string password)
        {
            return password;
        }
    }
}
