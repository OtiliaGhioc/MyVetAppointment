using FluentValidation;

namespace VetAppointment.Application.Commands
{
    public class CreateDrugCommandValidation : AbstractValidator<CreateDrugCommand>
    {
        public CreateDrugCommandValidation()
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
