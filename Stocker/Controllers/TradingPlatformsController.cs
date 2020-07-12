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
    public class TradingPlatformsController : ControllerBase
    {
        private readonly ILogger<TradingPlatformsController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMap<Database.Models.TradingPlatform, TradingPlatform> _tpDbToApiMapper;

        private readonly IMap<AddTradingPlatformRequest, Database.Models.TradingPlatform>
            _tpAddRequestToDbMapper;

        public TradingPlatformsController(ILogger<TradingPlatformsController> logger,
            StockerDbContext dbContext,
            IMap<Database.Models.TradingPlatform, TradingPlatform> tpDbToApiMapper,
            IMap<AddTradingPlatformRequest, Database.Models.TradingPlatform> tpAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _tpDbToApiMapper = tpDbToApiMapper;
            _tpAddRequestToDbMapper = tpAddRequestToDbMapper;
        }

        [HttpGet]
        public IEnumerable<TradingPlatform> Get([FromRoute] GetTradingPlatformsFilter filter)
        {
            var resultQuery = _dbContext.TradingPlatforms.Select(tp => tp);

            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                resultQuery = resultQuery.Where(tp =>
                    tp.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            foreach (var platform in resultQuery)
            {
                yield return _tpDbToApiMapper.Map(platform);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddTradingPlatformRequest request)
        {
            if (_dbContext.TradingPlatforms.Any(tp =>
                tp.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return BadRequest("TradingPlatform already exists.");
            }

            var tradingPlatform = _tpAddRequestToDbMapper.Map(request);
            await _dbContext.TradingPlatforms.AddAsync(tradingPlatform);
            await _dbContext.SaveChangesAsync();
            return Ok(new {Id = tradingPlatform.Id});
        }
    }
}