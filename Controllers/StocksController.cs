using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public StocksController(ILogger<StocksController> logger,
                                StockerDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
            _logger.LogInformation("StocksController instantiated.");
        }

        [HttpGet]
        public Task<IEnumerable<Stock>> Get([FromRoute]GetStocksFilter filter)
        {
            _logger.LogInformation("StocksController: Call reached GET method.");
            var tickerToFilter = filter?.Ticker?.ToUpper();
            var filteredStocks = _dbContext.Stocks.Include(s => s.Transactions).Where(s => s.Ticker.Equals(tickerToFilter));
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public Task<IActionResult> AddStock([FromBody]AddStockRequest request)
        {
            throw new NotImplementedException();
        }
    }
}