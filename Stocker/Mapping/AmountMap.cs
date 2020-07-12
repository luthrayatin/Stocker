
using Mapping;

namespace Stocker.Mapping
{
    class AmountDbToApiMap : IMap<Database.Models.Amount, Models.Api.Amount>
    {
        public Models.Api.Amount Map(Database.Models.Amount source)
        {
            return new Models.Api.Amount
            {
                CurrencyCode = source.Currency.Code,
                ValueMinor = source.ValueMinor,
                CurrencyId = source.CurrencyId
            };
        }
    }

    class AmountApiToDbMap : IMap<Models.Api.Amount, Database.Models.Amount>
    {
        public Database.Models.Amount Map(Models.Api.Amount source)
        {
            return new Database.Models.Amount
            {
                CurrencyId = source.CurrencyId,
                ValueMinor = source.ValueMinor
            };
        }
    }
}