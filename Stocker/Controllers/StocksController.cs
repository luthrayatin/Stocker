using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocker.Database;
using Stocker.Models.Api;
using Stock = Stocker.Database.Models.Stock;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly StockerDbContext _dbContext;
        private readonly ILogger<StocksController> _logger;
        private readonly IMap<AddStockRequest, Stock> _stockAddRequestToDbMapper;
        private readonly IMap<Stock, Models.Api.Stock> _stockDbToApiMapper;

        public StocksController(ILogger<StocksController> logger,
            StockerDbContext dbContext,
            IMap<Stock, Models.Api.Stock> stockDbToApiMapper,
            IMap<AddStockRequest, Stock> stockAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _stockDbToApiMapper = stockDbToApiMapper;
            _stockAddRequestToDbMapper = stockAddRequestToDbMapper;
        }

        [HttpGet]
        public IEnumerable<Models.Api.Stock> Get([FromQuery] GetStocksFilter filter)
        {
            var resultQuery = _dbContext.Stocks.Select(s => s);
            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                var lowerCaseFilterNameValue = filter.Name.ToLower();
                resultQuery = resultQuery.Where(s => s.Name.ToLower() == lowerCaseFilterNameValue);
            }

            if (!string.IsNullOrWhiteSpace(filter?.Ticker))
            {
                var lowerCaseFilterTickerValue = filter.Ticker.ToLower();
                resultQuery = resultQuery.Where(s => s.Ticker.ToLower() == lowerCaseFilterTickerValue);
            }

            foreach (var stock in resultQuery) yield return _stockDbToApiMapper.Map(stock);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddStockRequest request)
        {
            var lowerCaseName = request.Name.ToLower();
            var lowerCaseTicker = request.Ticker.ToLower();
            if (_dbContext.Stocks.Any(s => (s.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) ||
                                            s.Ticker.Equals(request.Ticker,
                                                StringComparison.CurrentCultureIgnoreCase)) &&
                                           s.StockExchangeId == request.StockExchangeId))
                return BadRequest("Stock already exists.");

            var stock = _stockAddRequestToDbMapper.Map(request);
            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new GetStocksFilter {Ticker = stock.Ticker});
        }
    }
}