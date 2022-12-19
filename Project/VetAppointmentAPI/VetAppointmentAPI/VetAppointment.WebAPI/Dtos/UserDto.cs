using VetAppointment.Domain.Entities;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;

namespace VetAppointment.WebAPI.Dtos
{
    namespace UserDto
    {
        public class DefaultUserDto
        {
            public Guid UserId { get; set; }
            public string? Username { get; set; }
            public string? Password { get; set; }
        }

        public class DetailUserDto
        {
            public DetailUserDto(User user)
            {
                UserId = user.UserId;
                Username = user.Username;
                IsMedic = user.IsMedic;
                HasOffice = user.HasOffice;
                OfficeId = user.OfficeId;
                JoinedDate = user.JoinedDate.Date.ToString("dd-MMM-yyyy");
            }

            public Guid UserId { get; private set; }
            public string Username { get; private set; }
            public bool IsMedic { get; private set; }
            public bool HasOffice { get; private set; }
            public Guid? OfficeId { get; private set; }
            public string JoinedDate { get; private set; }
        }

        public class CompleteUserDto
        {
            public CompleteUserDto(User user, List<Appointment> appointments, List<User> appointers)
            {
                UserId = user.UserId;
                Username = user.Username;
                IsMedic = user.IsMedic;
                HasOffice = user.HasOffice;
                OfficeId = user.OfficeId;
                JoinedDate = user.JoinedDate.Date.ToString("dd-MMM-yyyy");

                if (appointments.Count != appointers.Count)
                    return;

                for (int i = 0; i < appointments.Count; i++)
                    Appointments.Add(new AppointmentEssentialOnlyDto(appointments[i], appointers[i]));
            }

            public Guid UserId { get; private set; }
            public string Username { get; private set; }
            public bool IsMedic { get; private set; }
            public bool HasOffice { get; private set; }
            public Guid? OfficeId { get; private set; }
            public string JoinedDate { get; private set; }

            public List<AppointmentEssentialOnlyDto> Appointments { get; private set; } = new List<AppointmentEssentialOnlyDto>();
        }

        public class UserAppointmentsDto
        {
            public UserAppointmentsDto(User user, List<Appointment> appointments, List<User> appointers)
            {
                UserId = user.UserId;
                Username = user.Username;
                IsMedic = user.IsMedic;

                if (appointments.Count != appointers.Count)
                    return;

                for (int i = 0; i < appointments.Count; i++)
                    Appointments.Add(new AppointmentEssentialOnlyDto(appointments[i], appointers[i]));
            }

            public Guid UserId { get; private set; }
            public string Username { get; private set; }
            public bool IsMedic { get; private set; }

            public List<AppointmentEssentialOnlyDto> Appointments { get; private set; } = new List<AppointmentEssentialOnlyDto>();
        }

        public class UserMedicalHistoryDto
        {
            public UserMedicalHistoryDto(User user, List<MedicalHistoryEntry> medicalHistoryEntries, List<User> appointers)
            {
                UserId = user.UserId;
                Username = user.Username;
                IsMedic = user.IsMedic;

                if (medicalHistoryEntries.Count != appointers.Count)
                    return;

                for (int i = 0; i < medicalHistoryEntries.Count; i++)
                    MedicalHistoryEntries.Add(new MedicalEntryEssentialOnlyDto(medicalHistoryEntries[i], appointers[i]));
            }

            public Guid UserId { get; private set; }
            public string Username { get; private set; }
            public bool IsMedic { get; private set; }

            public List<MedicalEntryEssentialOnlyDto> MedicalHistoryEntries { get; private set; } = new List<MedicalEntryEssentialOnlyDto>();
        }
    }

}
