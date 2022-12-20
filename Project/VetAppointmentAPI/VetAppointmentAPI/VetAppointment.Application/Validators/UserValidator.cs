using FluentValidation;
using VetAppointment.WebAPI.Dtos.UserDto;

namespace VetAppointment.WebAPI.Validators
{
    public class UserValidator : AbstractValidator<DefaultUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username can not be empty or null")
                .Length(3, 10)
                .WithMessage("Username should be between 3 and 10 characters");
            RuleFor(x=>x.Password)
                .NotEmpty()
                .WithMessage("Password can not be empty or null")
                .MinimumLength(5)
                .WithMessage("Password should have at least 5 characters");
        }
    }
}
