using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public StocksController(ILogger<StocksController> logger,
                                StockerDbContext dbContext,
                                IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Stock> Get([FromRoute]GetStocksFilter filter)
        {
            var resultQuery = _dbContext.Stocks.Select(s => s);
            if(!string.IsNullOrWhiteSpace(filter?.Name)){
                resultQuery = resultQuery.Where(s => s.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }
            if(!string.IsNullOrWhiteSpace(filter?.Ticker)){
                resultQuery = resultQuery.Where(s => s.Ticker.Equals(filter.Ticker, StringComparison.CurrentCultureIgnoreCase));
            }
            
            return _mapper.Map<IEnumerable<Database.Models.Stock>, IEnumerable<Models.Api.Stock>>(resultQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody]AddStockRequest request)
        {
            if(_dbContext.Stocks.Any(s => (s.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) ||
            s.Ticker.Equals(request.Ticker, StringComparison.CurrentCultureIgnoreCase)) &&
            s.StockExchangeId == request.StockExchangeId)){
                return BadRequest("Stock already exists.");
            }
            
            var stock = _mapper.Map<AddStockRequest, Database.Models.Stock>(request);
            _dbContext.Stocks.Add(stock);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Id = stock.Id });
        }
    }
}