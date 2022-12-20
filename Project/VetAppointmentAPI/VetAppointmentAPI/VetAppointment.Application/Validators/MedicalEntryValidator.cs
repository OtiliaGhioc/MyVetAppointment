using FluentValidation;
using VetAppointment.WebAPI.Dtos.MedicalEntryDto;

namespace VetAppointment.WebAPI.Validators
{
    public class MedicalEntryValidator:AbstractValidator<MedicalEntryCreateDto>
    {
        public MedicalEntryValidator()
        {
            RuleFor(c=>c.AppointmentId)
                .NotEmpty()
                .WithMessage("AppointmentId can not be empty or null");
            RuleFor(c=>c.PrescriptionId)
                .NotEmpty()
                .WithMessage("PrescriptionId can not be empty or null");
        }
    }
}
