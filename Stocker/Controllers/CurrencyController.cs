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
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMap<Database.Models.Currency, Models.Api.Currency> _currencyDbToApiMapper;
        private readonly IMap<Models.Api.AddCurrencyRequest, Database.Models.Currency> _currencyAddRequestToDbMapper;
        public CurrencyController(ILogger<CurrencyController> logger, StockerDbContext dbContext, IMap<Database.Models.Currency, Currency> currencyDbToApiMapper, IMap<AddCurrencyRequest, Database.Models.Currency> currencyAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _currencyDbToApiMapper = currencyDbToApiMapper;
            _currencyAddRequestToDbMapper = currencyAddRequestToDbMapper;
        }

        /// <summary>
        /// Get currencies based on the input filter.
        /// </summary>
        /// <param name="filter">Specify Code and/or Name of currencies to fetch</param>
        /// <returns>Collection of currencies filtered by input.</returns>
        [HttpGet]
        public IEnumerable<Currency> Get([FromRoute] GetCurrencyFilter filter)
        {
            var resultQuery = _dbContext.Currencies.Select(c => c);

            if (!string.IsNullOrWhiteSpace(filter?.Code))
            {
                resultQuery = resultQuery.Where(c => c.Code.Equals(filter.Code, StringComparison.CurrentCultureIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                resultQuery = resultQuery.Where(c => c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            foreach (var currency in resultQuery)
            {
                yield return _currencyDbToApiMapper.Map(currency);
            }
        }

        /// <summary>
        /// Add a new currency
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddCurrencyRequest request)
        {
            if (_dbContext.Currencies.Any(c => c.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) ||
             c.Code.Equals(request.Code, StringComparison.CurrentCultureIgnoreCase)))
            {
                return BadRequest("Currency already exists.");
            }

            var currency = _currencyAddRequestToDbMapper.Map(request);
            _dbContext.Currencies.Add(currency);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Code = currency.Code }, currency);
        }
    }
}