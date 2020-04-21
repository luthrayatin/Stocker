using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AddStockExchangeRequestValidator : AbstractValidator<AddStockExchangeRequest>
    {
        public AddStockExchangeRequestValidator()
        {
            RuleFor(r => r.Country).NotEmpty();
            RuleFor(r => r.CurrencyId).GreaterThan(0);
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}