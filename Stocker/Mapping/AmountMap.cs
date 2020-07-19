using Mapping;
using Stocker.Database.Models;

namespace Stocker.Mapping
{
    internal class AmountDbToApiMap : IMap<Amount, Models.Api.Amount>
    {
        public Models.Api.Amount Map(Amount source)
        {
            return new Models.Api.Amount
            {
                CurrencyCode = source.Currency.Code,
                ValueMinor = source.ValueMinor,
                CurrencyId = source.CurrencyId
            };
        }
    }

    internal class AmountApiToDbMap : IMap<Models.Api.Amount, Amount>
    {
        public Amount Map(Models.Api.Amount source)
        {
            return new Amount
            {
                CurrencyId = source.CurrencyId,
                ValueMinor = source.ValueMinor
            };
        }
    }
}