using VetAppointment.Domain.Entities;

namespace VetAppointment.WebAPI.Dtos
{
    namespace AppointmentDtos
    {
        public class AppointmentCreateDto
        {
            public AppointmentCreateDto(Guid appointerId, Guid appointeeId, string description, string type)
            {
                AppointerId = appointerId;
                AppointeeId = appointeeId;
                Description = description;
                Type = type;
            }

            public Guid AppointerId { get; set; }
            public Guid AppointeeId { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
        }

        public class AppointmentModifyDto
        {
            public AppointmentModifyDto(DateTime? dateTime, string? description, string? type, bool? isExpired)
            {
                DateTime = dateTime;
                Description = description;
                Type = type;
                IsExpired = isExpired;
            }

            public DateTime? DateTime { get; set; }
            public string? Description { get; set; }
            public string? Type { get; set; }
            public bool? IsExpired { get; set; }

            public Appointment ApplyModificationsToModel(Appointment appointment)
            {
                if(DateTime != null)
                    appointment.DateTime = (DateTime)DateTime;
                if (Description != null)
                    appointment.Description = (string)Description;
                if (Type != null)
                    appointment.Type = (string)Type;
                if (IsExpired != null)
                    appointment.IsExpired = (bool)IsExpired;
                return appointment;
            }
        }

        public class AppointmentDetailDto
        {
            public AppointmentDetailDto(Appointment appointment)
            {
                AppointmentId = appointment.AppointmentId;
                AppointerId = appointment.AppointerId;
                AppointeeId = appointment.AppointeeId;
                Description = appointment.Description;
                Type = appointment.Type;
                IsExpired = appointment.IsExpired;
                DateTime = appointment.DateTime;
            }

            public Guid AppointmentId { get; private set; }
            public Guid AppointerId { get; private set; }
            public Guid AppointeeId { get; private set; }
            public string Description { get; private set; }
            public string Type { get; private set; }
            public bool IsExpired { get; private set; }
            public DateTime DateTime { get; private set; }
        }
    }
}
