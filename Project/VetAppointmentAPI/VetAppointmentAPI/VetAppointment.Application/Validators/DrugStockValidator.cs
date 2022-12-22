using FluentValidation;
using VetAppointment.WebAPI.DTOs;

namespace VetAppointment.WebAPI.Validators
{
    public class DrugStockValidator: AbstractValidator<CreateDrugStockDto>
    {
        public DrugStockValidator()
        {
            RuleFor(x=>x.TypeId)
                .NotEmpty()
                .WithMessage("TypeId can not be empty or null");
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .WithMessage("Quantity can not be empty or null")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity should be greater or equal to 0");
            RuleFor(x => x.PricePerItem)
                .NotNull()
                .NotEmpty()
                .WithMessage("Price can not be empty or null")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Price should be greater or equal to 1");
        }
    }
}
