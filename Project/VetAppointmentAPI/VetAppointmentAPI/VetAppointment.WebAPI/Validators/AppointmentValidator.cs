using FluentValidation;
using VetAppointment.WebAPI.Dtos.AppointmentDtos;

namespace VetAppointment.WebAPI.Validators
{
    public class AppointmentValidator:AbstractValidator<AppointmentCreateDto>
    {
        public AppointmentValidator()
        {
            RuleFor(x=>x.AppointeeId)
                .NotEmpty()
                .WithMessage("AppointeeId can not be empty or null");
            RuleFor(x=>x.AppointerId)
                .NotEmpty()
                .WithMessage("AppointerId can not be empty or null");
            RuleFor(x=>x.Title)
                .NotEmpty()
                .WithMessage("Title can not be empty or null")
                .Length(3,10)
                .WithMessage("Title should be between 3 and 10 characters");
            RuleFor(x=>x.Description)
                .NotEmpty()
                .WithMessage("Description can not be empty or null")
                .MaximumLength(50)
                .WithMessage("Description have maximum 50 characters");
            RuleFor(x=>x.DueDate)
                .NotEmpty()
                .WithMessage("DueDate can not be empty or null")
                .GreaterThanOrEqualTo(DateTime.Now)
                .WithMessage("DueDate should be in the future");
            RuleFor(x=>x.Type)
                .NotEmpty()
                .WithMessage("Type can not be empty or null");
        }
    }
}
