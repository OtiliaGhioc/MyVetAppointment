using FluentValidation;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Validators
{
    public class DrugValidator : AbstractValidator<CreateDrugDto>
    {
        public DrugValidator()
        {
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price can not be empty or null")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Price should be greater or equal to 1");
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title can not be empty or null")
                .MinimumLength(3)
                .WithMessage("Title should have at least 3 characters");
        }
    }
}
