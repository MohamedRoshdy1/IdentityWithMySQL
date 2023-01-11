using FluentValidation;
using IdenittyWithMySql.Areas.CP.Models;

namespace Identity.API.Validations
{
    public class ProductsValidators:AbstractValidator<Products>
    {
        public ProductsValidators()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(3).WithMessage("Min Lenght is 3");
          
        }
    }
}
