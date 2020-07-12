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
    public class StockExchangesController : ControllerBase
    {
        private readonly ILogger<StockExchangesController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMap<Database.Models.StockExchange, Models.Api.StockExchange> _stockExchangeDbToApiMapper;
        private readonly IMap<Models.Api.AddStockExchangeRequest, Database.Models.StockExchange> _stockExchangeAddRequestToDbMapper;
        public StockExchangesController(ILogger<StockExchangesController> logger,
        StockerDbContext dbContext, IMap<Database.Models.StockExchange, StockExchange> stockExchangeDbToApiMapper, IMap<AddStockExchangeRequest, Database.Models.StockExchange> stockExchangeAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _stockExchangeDbToApiMapper = stockExchangeDbToApiMapper;
            _stockExchangeAddRequestToDbMapper = stockExchangeAddRequestToDbMapper;
        }

        [HttpGet]
        public IEnumerable<StockExchange> Get([FromRoute] GetStockExchangesFilter filter)
        {
            var resultQuery = _dbContext.StockExchanges.Select(se => se);

            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                resultQuery = resultQuery.Where(se => se.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter?.Country))
            {
                resultQuery = resultQuery.Where(se => se.Country.Equals(filter.Country, StringComparison.CurrentCultureIgnoreCase));
            }

            foreach (var stockExchange in resultQuery)
            {
                yield return _stockExchangeDbToApiMapper.Map(stockExchange);   
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddStockExchangeRequest request)
        {
            var stockExchangeExists = _dbContext.StockExchanges
            .Any(se => se.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) &&
            se.Country.Equals(request.Country, StringComparison.CurrentCultureIgnoreCase));

            if (stockExchangeExists)
            {
                return BadRequest($"StockExchange with Name: \"{request.Name}\" already exists.");
            }

            var currency = _dbContext.Currencies.FirstOrDefault(c => c.Id == request.CurrencyId);
            if (currency == null)
            {
                return BadRequest($"The CurrencyId: {request.CurrencyId} is invalid.");
            }

            var stockExchange = _stockExchangeAddRequestToDbMapper.Map(request);
            _dbContext.StockExchanges.Add(stockExchange);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Id = stockExchange.Id });
        }
    }
}