using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.WebAPI.Dtos
{
    namespace UserDto
    {
        public class DefaultUserDto
        {
            public Guid UserId { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class CompleteUserDto
        {
            public CompleteUserDto(User user, List<Appointment> appointments, List<User> appointers)
            {
                UserId = user.UserId;
                Username = user.Username;
                HasOffice = user.HasOffice;
                OfficeId = user.OfficeId;

                if (appointments.Count != appointers.Count)
                    return;

                for(int i = 0;i< appointments.Count;i++)
                    Appointments.Add(new AppointmentEssentialOnlyDto(appointments[i], appointers[i]));
            }

            public Guid UserId { get; private set; }
            public string Username { get; private set; }
            public bool HasOffice { get; private set; }
            public Guid OfficeId { get; private set; }

            public List<AppointmentEssentialOnlyDto> Appointments { get; private set; } = new List<AppointmentEssentialOnlyDto>();
        }
    }

}
