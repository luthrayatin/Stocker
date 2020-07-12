using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AddStockRequestValidator : AbstractValidator<AddStockRequest>
    {
        public AddStockRequestValidator()
        {
            RuleFor(r => r.StockExchangeId).GreaterThan(0);
            RuleFor(r => r.Ticker).NotEmpty();
        }
    }
}