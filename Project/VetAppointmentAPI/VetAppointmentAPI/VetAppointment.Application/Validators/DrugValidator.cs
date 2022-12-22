using FluentValidation;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Validators
{
    public class DrugValidator : AbstractValidator<CreateDrugDto>
    {
        public DrugValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title can not be empty or null")
                .MinimumLength(3)
                .WithMessage("Title should have at least 3 characters");
        }
    }
}
