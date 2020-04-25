using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AddCurrencyRequestValidator : AbstractValidator<AddCurrencyRequest>
    {
        public AddCurrencyRequestValidator()
        {
            RuleFor(req => req.Code).NotEmpty();
            RuleFor(req => req.Name).NotEmpty();
            RuleFor(req => req.MinorToMajorMultiplier).GreaterThanOrEqualTo(1);
        }
    }
}