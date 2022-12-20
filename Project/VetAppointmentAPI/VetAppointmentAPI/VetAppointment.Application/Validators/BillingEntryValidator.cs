using FluentValidation;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Validators
{
    public class BillingEntryValidator:AbstractValidator<BillingEntryDto>
    {
        public BillingEntryValidator()
        {
            RuleFor(x=>x.AppointmentId)
                .NotEmpty()
                .WithMessage("AppointmentId can not be empty or null");
            RuleFor(x => x.IssuerId)
                .NotEmpty()
                .WithMessage("IssuerId can not be empty or null");
            RuleFor(x => x.CustomerId)
                .NotEmpty()
                .WithMessage("CustomerId can not be empty or null");
            RuleFor(x=>x.PrescriptionId)
                .NotEmpty()
                .WithMessage("PrescriptionId can not be empty or null");
            RuleFor(x=>x.Price)
                .NotEmpty()
                .WithMessage("Price can not be empty or null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price should be greater or equal to 0");
            RuleFor(x => x.DateTime)
                .NotEmpty()
                .WithMessage("DateTime can not be empty or null");
        }
    }
}
