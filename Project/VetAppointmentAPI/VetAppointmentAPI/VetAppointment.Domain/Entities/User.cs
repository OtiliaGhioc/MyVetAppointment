using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class User
    {
        public User(string username, string password, bool isMedic=false)
        {
            UserId = Guid.NewGuid();
            Username = username;
            Password = password;
            IsMedic = isMedic;
            JoinedDate= DateTime.Now;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsMedic { get; private set; }
        public bool HasOffice { get; private set; } = false;
        public Guid? OfficeId { get; private set; }
        public Office? UserOffice { get; private set; }
        public DateTime JoinedDate { get; private set; }

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
            OfficeId = null;
            HasOffice = false;
            return Result.Success();
        }

        public bool IsPasswordValid(string password, string? secret = null)
        {
            return PasswordHasher.IsPasswordValid(password, Password, secret);
        }
    }
}
