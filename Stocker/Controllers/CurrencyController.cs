using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Stocker.Database;
using Stocker.Models.Api;
using Currency = Stocker.Database.Models.Currency;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IMap<AddCurrencyRequest, Currency> _currencyAddRequestToDbMapper;
        private readonly IMap<Currency, Models.Api.Currency> _currencyDbToApiMapper;
        private readonly StockerDbContext _dbContext;
        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger, StockerDbContext dbContext,
            IMap<Currency, Models.Api.Currency> currencyDbToApiMapper,
            IMap<AddCurrencyRequest, Currency> currencyAddRequestToDbMapper,
            IConfiguration configuration)
        {
            _logger = logger;
            _dbContext = dbContext;
            _currencyDbToApiMapper = currencyDbToApiMapper;
            _currencyAddRequestToDbMapper = currencyAddRequestToDbMapper;
            _logger.LogInformation("Using DB Connection String as: {ConnectionString}", configuration.GetConnectionString("StockerDb"));
        }

        /// <summary>
        ///     Get currencies based on the input filter.
        /// </summary>
        /// <param name="filter">Specify Code and/or Name of currencies to fetch</param>
        /// <returns>Collection of currencies filtered by input.</returns>
        [HttpGet]
        public IEnumerable<Models.Api.Currency> Get([FromRoute] GetCurrencyFilter filter)
        {
            var resultQuery = _dbContext.Currencies.Select(c => c);

            if (!string.IsNullOrWhiteSpace(filter?.Code))
                resultQuery = resultQuery.Where(c =>
                    c.Code.Equals(filter.Code, StringComparison.CurrentCultureIgnoreCase));

            if (!string.IsNullOrWhiteSpace(filter?.Name))
                resultQuery = resultQuery.Where(c =>
                    c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            foreach (var currency in resultQuery) yield return _currencyDbToApiMapper.Map(currency);
        }

        /// <summary>
        ///     Add a new currency
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddCurrencyRequest request)
        {
            if (_dbContext.Currencies.Any(c => c.Name.ToLower() == request.Name.ToLower() ||
                                               c.Code.ToLower() == request.Code.ToLower()))
                return BadRequest("Currency already exists.");

            var currency = _currencyAddRequestToDbMapper.Map(request);
            await _dbContext.Currencies.AddAsync(currency);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {currency.Code}, currency);
        }
    }
}