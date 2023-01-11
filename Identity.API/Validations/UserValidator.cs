using FluentValidation;
using Identity.API.DTOs;

namespace Identity.API.Validations
{
    public class UserValidator:AbstractValidator<userDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(2).WithMessage("Min Lenght is 2");
            RuleFor(x=>x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(6);
        }
    }
}
