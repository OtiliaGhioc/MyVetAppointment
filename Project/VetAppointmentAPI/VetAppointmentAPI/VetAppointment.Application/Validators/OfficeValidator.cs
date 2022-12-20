using FluentValidation;
using VetAppointment.WebAPI.Dtos;

namespace VetAppointment.WebAPI.Validators
{
    public class OfficeValidator:AbstractValidator<OfficeDto>
    {
        public OfficeValidator()
        {
            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address can not be empty or null")
                .Matches(@"[a-zA-Z\s]+ (\d{1,})(\,)? [A-Z]{2}(\,)? [0-9]{6}")
                .WithMessage("Address does not respect the naming convention. It should be similar to \"Strada Zorilor 13, IS, 123456\".");

        }
    }
}
