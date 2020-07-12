using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocker.Database;
using Stocker.Models.Api;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMap<Database.Models.Stock, Stock> _stockDbToApiMapper;
        private readonly IMap<AddStockRequest, Database.Models.Stock> _stockAddRequestToDbMapper;

        public StocksController(ILogger<StocksController> logger,
            StockerDbContext dbContext,
            IMap<Database.Models.Stock, Stock> stockDbToApiMapper,
            IMap<AddStockRequest, Database.Models.Stock> stockAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _stockDbToApiMapper = stockDbToApiMapper;
            _stockAddRequestToDbMapper = stockAddRequestToDbMapper;
        }

        [HttpGet]
        public IEnumerable<Stock> Get([FromRoute] GetStocksFilter filter)
        {
            var resultQuery = _dbContext.Stocks.Select(s => s);
            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                resultQuery = resultQuery.Where(s =>
                    s.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter?.Ticker))
            {
                resultQuery = resultQuery.Where(s =>
                    s.Ticker.Equals(filter.Ticker, StringComparison.CurrentCultureIgnoreCase));
            }

            foreach (var stock in resultQuery)
            {
                yield return _stockDbToApiMapper.Map(stock);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddStockRequest request)
        {
            if (_dbContext.Stocks.Any(s => (s.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) ||
                                            s.Ticker.Equals(request.Ticker,
                                                StringComparison.CurrentCultureIgnoreCase)) &&
                                           s.StockExchangeId == request.StockExchangeId))
            {
                return BadRequest("Stock already exists.");
            }

            var stock = _stockAddRequestToDbMapper.Map(request);
            await _dbContext.Stocks.AddAsync(stock);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new GetStocksFilter{ Ticker = stock.Ticker });
        }
    }
}