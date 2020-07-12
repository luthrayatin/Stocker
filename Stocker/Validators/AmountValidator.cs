using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AmountValidator : AbstractValidator<Amount>
    {
        public AmountValidator()
        {
            RuleFor(a => a.ValueMinor).NotEqual(0);
            RuleFor(a => a.CurrencyCode).NotEmpty();
        }
    }
}