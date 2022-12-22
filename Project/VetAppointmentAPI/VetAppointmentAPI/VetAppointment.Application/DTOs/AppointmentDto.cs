using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.Dtos
{
    namespace AppointmentDtos
    {
        public class AppointmentCreateDto
        {
            public AppointmentCreateDto(Guid appointerId, Guid appointeeId, DateTime dueDate, string title, string description, string type)
            {
                AppointerId = appointerId;
                AppointeeId = appointeeId;
                DueDate = dueDate;
                Description = description;
                Title = title;
                Type = type;
            }

            public Guid AppointerId { get; set; }
            public Guid AppointeeId { get; set; }
            public DateTime DueDate { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
        }

        public class AppointmentModifyDto
        {
            public AppointmentModifyDto(DateTime? dueDate, string? description, string? title, string? type, bool? isExpired)
            {
                DueDate = dueDate;
                Description = description;
                Title = title;
                Type = type;
                IsExpired = isExpired;
            }

            public DateTime? DueDate { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Type { get; set; }
            public bool? IsExpired { get; set; }

            public Appointment ApplyModificationsToModel(Appointment appointment)
            {
                if (DueDate != null)
                    appointment.DueDate = (DateTime)DueDate;
                if (Title != null)
                    appointment.Title = Title;
                if (Description != null)
                    appointment.Description = Description;
                if (Type != null)
                    appointment.Type = Type;
                if (IsExpired != null)
                    appointment.IsExpired = (bool)IsExpired;
                return appointment;
            }
        }

        public class AppointmentDetailDto
        {
            public AppointmentDetailDto() { }
            public AppointmentDetailDto(Appointment appointment)
            {
                AppointmentId = appointment.AppointmentId;
                AppointerId = appointment.AppointerId;
                AppointeeId = appointment.AppointeeId;
                Description = appointment.Description;
                Title = appointment.Title;
                Type = appointment.Type;
                IsExpired = appointment.IsExpired;
                CreatedAt = appointment.CreatedAt;
                DueDate = appointment.DueDate;
            }

            public Guid AppointmentId { get; private set; }
            public Guid AppointerId { get; private set; }
            public Guid AppointeeId { get; private set; }
            public string Title { get; private set; }
            public string Description { get; private set; }
            public string Type { get; private set; }
            public bool IsExpired { get; private set; }
            public DateTime CreatedAt { get; private set; }
            public DateTime DueDate { get; private set; }
        }

        public class AppointmentEssentialOnlyDto
        {
            public AppointmentEssentialOnlyDto(Appointment appointment, User appointer)
            {
                AppointmentId = appointment.AppointmentId;
                Appointer = appointer.Username;
                Title = appointment.Title;
                DueDate = appointment.DueDate.ToString("dd-MMM-yyyy");
                DueTime = appointment.DueDate.ToString("HH:mm");
            }

            public Guid AppointmentId { get; private set; }
            public string Appointer { get; private set; }
            public string Title { get; private set; }
            public string DueDate { get; private set; }
            public string DueTime { get; private set; }
        }

        public class AppontmentEssentialExtendedDto
        {
            public AppontmentEssentialExtendedDto(Appointment appointment, User appointer, User appointee)
            {
                AppointmentId = appointment.AppointmentId;
                Appointer = appointer.Username;
                Appointee = appointee.Username;
                Title = appointment.Title;
                DueDate = appointment.DueDate.Date.ToString("dd-MMM-yyyy");
                Description = appointment.Description;
            }

            public Guid AppointmentId { get; private set; }
            public string Appointer { get; private set; }
            public string Appointee { get; private set; }
            public string Title { get; private set; }
            public string DueDate { get; private set; }
            public string Description { get; private set; }
        }
    }
}
