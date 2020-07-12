using Mapping;

namespace Stocker.Mapping
{
    public class CurrencyApiToDbMap : IMap<Models.Api.Currency, Database.Models.Currency>
    {
        public Database.Models.Currency Map(Models.Api.Currency source)
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

    public class CurrencyDbToApiMap : IMap<Database.Models.Currency, Models.Api.Currency>
    {
        public Models.Api.Currency Map(Database.Models.Currency source)
        {
            return new Models.Api.Currency
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