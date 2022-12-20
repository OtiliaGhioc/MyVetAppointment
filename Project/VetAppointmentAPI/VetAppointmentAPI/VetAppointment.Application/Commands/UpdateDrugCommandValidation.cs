using FluentValidation;

namespace VetAppointment.Application.Commands
{
    public class UpdateDrugCommandValidation : AbstractValidator<UpdateDrugCommand>
    {
        public UpdateDrugCommandValidation()
        {
            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price can not be empty or null")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Price should be greater or equal to 1");
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .WithMessage("Title can not be empty or null")
                .MinimumLength(3)
                .WithMessage("Title should have at least 3 characters");
        }
    }
}
