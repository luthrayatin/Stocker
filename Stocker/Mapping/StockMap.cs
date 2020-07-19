using Mapping;
using Stocker.Models.Api;
using Stock = Stocker.Database.Models.Stock;
using StockExchange = Stocker.Database.Models.StockExchange;

namespace Stocker.Mapping
{
    public class StockDbToApiMap : IMap<Stock, Models.Api.Stock>
    {
        private readonly IMap<StockExchange, Models.Api.StockExchange> _stockExchangeMapper;

        public StockDbToApiMap(IMap<StockExchange, Models.Api.StockExchange> stockExchangeMapper)
        {
            _stockExchangeMapper = stockExchangeMapper;
        }

        public Models.Api.Stock Map(Stock source)
        {
            return new Models.Api.Stock
            {
                Id = source.Id,
                Name = source.Name,
                StockExchange = _stockExchangeMapper.Map(source.StockExchange),
                Ticker = source.Ticker
            };
        }
    }

    public class StockAddRequestToDbMap : IMap<AddStockRequest, Stock>
    {
        public Stock Map(AddStockRequest source)
        {
            return new Stock
            {
                Name = source.Name,
                StockExchangeId = source.StockExchangeId,
                Ticker = source.Ticker
            };
        }
    }
}