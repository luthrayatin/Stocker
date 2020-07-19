using Mapping;
using Stocker.Models.Api;

namespace Stocker.Mapping
{
    public class CurrencyAddRequestToDbMap : IMap<AddCurrencyRequest, Database.Models.Currency>
    {
        public Database.Models.Currency Map(AddCurrencyRequest source)
        {
            return new Database.Models.Currency
            {
                Code = source.Code,
                MinorToMajorMultiplier = source.MinorToMajorMultiplier,
                Symbol = source.Symbol,
                Name = source.Name
            };
        }
    }

    public class CurrencyDbToApiMap : IMap<Database.Models.Currency, Currency>
    {
        public Currency Map(Database.Models.Currency source)
        {
            return new Currency
            {
                Id = source.Id,
                Code = source.Code,
                MinorToMajorMultiplier = source.MinorToMajorMultiplier,
                Name = source.Name,
                Symbol = source.Symbol
            };
        }
    }
}