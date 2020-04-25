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
    public class CurrencyController : ControllerBase
    {
        private readonly ILogger<CurrencyController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMapper _mapper;
        public CurrencyController(ILogger<CurrencyController> logger, StockerDbContext dbContext, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Currency> Get([FromRoute]GetCurrencyFilter filter)
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

            return _mapper.Map<IEnumerable<Database.Models.Currency>, IEnumerable<Currency>>(resultQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody]AddCurrencyRequest request)
        {
            if (_dbContext.Currencies.Any(c => c.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase) ||
             c.Code.Equals(request.Code, StringComparison.CurrentCultureIgnoreCase)))
            {
                return BadRequest("Currency already exists.");
            }

            var currency = _mapper.Map<AddCurrencyRequest, Database.Models.Currency>(request);
            _dbContext.Currencies.Add(currency);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Id = currency.Id });
        }
    }
}