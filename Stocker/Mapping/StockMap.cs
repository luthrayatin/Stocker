using Mapping;

namespace Stocker.Mapping
{
    public class StockDbToApiMap : IMap<Database.Models.Stock, Models.Api.Stock>
    {
        private readonly IMap<Database.Models.StockExchange, Models.Api.StockExchange> _stockExchangeMapper;

        public StockDbToApiMap(IMap<Database.Models.StockExchange, Models.Api.StockExchange> stockExchangeMapper)
        {
            _stockExchangeMapper = stockExchangeMapper;
        }

        public Models.Api.Stock Map(Database.Models.Stock source)
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

    public class StockAddRequestToDbMap : IMap<Models.Api.AddStockRequest, Database.Models.Stock>
    {
        public Database.Models.Stock Map(Models.Api.AddStockRequest source)
        {
            return new Database.Models.Stock
            {
                Name = source.Name,
                StockExchangeId = source.StockExchangeId,
                Ticker = source.Ticker
            };
        }
    }
}