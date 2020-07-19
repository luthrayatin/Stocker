using Mapping;
using Stocker.Models.Api;
using Currency = Stocker.Database.Models.Currency;
using StockExchange = Stocker.Database.Models.StockExchange;

namespace Stocker.Mapping
{
    public class
        StockExchangeAddRequestToDbMap : IMap<AddStockExchangeRequest, StockExchange>
    {
        public StockExchange Map(AddStockExchangeRequest source)
        {
            return new StockExchange
            {
                Country = source.Country,
                CurrencyId = source.CurrencyId,
                Name = source.Name
            };
        }
    }

    public class StockExchangeDbToApiMap : IMap<StockExchange, Models.Api.StockExchange>
    {
        private readonly IMap<Currency, Models.Api.Currency> _currencyMapper;

        public StockExchangeDbToApiMap(IMap<Currency, Models.Api.Currency> currencyMapper)
        {
            _currencyMapper = currencyMapper;
        }

        public Models.Api.StockExchange Map(StockExchange source)
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