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
    public class StockExchangesController : ControllerBase
    {
        private readonly ILogger<StockExchangesController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMapper _mapper;
        public StockExchangesController(ILogger<StockExchangesController> logger,
        StockerDbContext dbContext,
        IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<StockExchange> Get([FromRoute]GetStockExchangesFilter filter)
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

            return _mapper.Map<IEnumerable<Database.Models.StockExchange>, IEnumerable<StockExchange>>(resultQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody]AddStockExchangeRequest request)
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

            var stockExchange = _mapper.Map<AddStockExchangeRequest, Database.Models.StockExchange>(request);
            _dbContext.StockExchanges.Add(stockExchange);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Id = stockExchange.Id });
        }
    }
}