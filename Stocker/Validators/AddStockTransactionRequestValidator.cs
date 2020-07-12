using System;
using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AddStockTransactionRequestValidator : AbstractValidator<AddStockTransactionRequest>
    {
        public AddStockTransactionRequestValidator()
        {
            RuleFor(req => req.StockId).GreaterThan(0);
            RuleFor(req => req.Quantity).GreaterThanOrEqualTo(1);
            RuleFor(req => req.PricePerUnit).SetValidator(new AmountValidator());
            RuleFor(req => req.ConversionRate).GreaterThan(0);
            RuleFor(req => req.Commission).SetValidator(new AmountValidator());
            RuleFor(req => req.TransactionDate).NotNull().GreaterThan(DateTimeOffset.MinValue);
            RuleFor(req => req.StockExchangeId).GreaterThan(0);
            RuleFor(req => req.TradingPlatformId).GreaterThan(0);
            RuleFor(req => req.UserCurrencyId).GreaterThan(0);
        }
    }
}