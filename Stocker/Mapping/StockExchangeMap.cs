using Mapping;

namespace Stocker.Mapping
{
    public class StockExchangeAddRequestToDbMap : IMap<Models.Api.AddStockExchangeRequest, Database.Models.StockExchange>
    {
        public Database.Models.StockExchange Map(Models.Api.AddStockExchangeRequest source)
        {
            return new Database.Models.StockExchange
            {
                Country = source.Country,
                CurrencyId = source.CurrencyId,
                Name = source.Name
            };
        }
    }

    public class StockExchangeDbToApiMap : IMap<Database.Models.StockExchange, Models.Api.StockExchange>
    {
        private readonly IMap<Database.Models.Currency, Models.Api.Currency> _currencyMapper;

        public StockExchangeDbToApiMap(IMap<Database.Models.Currency, Models.Api.Currency> currencyMapper)
        {
            _currencyMapper = currencyMapper;
        }

        public Models.Api.StockExchange Map(Database.Models.StockExchange source)
        {
            return new Models.Api.StockExchange
            {
                Id = source.Id,
                Country = source.Country,
                Name = source.Name,
                Currency = _currencyMapper.Map(source.Currency)
            };
        }
    }
}