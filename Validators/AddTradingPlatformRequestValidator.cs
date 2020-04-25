using FluentValidation;
using Stocker.Models.Api;

namespace Stocker.Validators
{
    public class AddTradingPlatformRequestValidator : AbstractValidator<AddTradingPlatformRequest>
    {
        public AddTradingPlatformRequestValidator()
        {
            RuleFor(req => req.Name).NotEmpty();
        }
    }
}