using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class CurrencyValidator : AbstractValidator<Currency>
    {
        public CurrencyValidator()
        {
            RuleFor(c => c.Code).NotEmpty();
            RuleFor(c => c.MinorToMajorMultiplier).GreaterThanOrEqualTo(1);
        }
    }
}