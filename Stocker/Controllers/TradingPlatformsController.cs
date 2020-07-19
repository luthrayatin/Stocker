using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocker.Database;
using Stocker.Models.Api;
using TradingPlatform = Stocker.Database.Models.TradingPlatform;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradingPlatformsController : ControllerBase
    {
        private readonly StockerDbContext _dbContext;
        private readonly ILogger<TradingPlatformsController> _logger;

        private readonly IMap<AddTradingPlatformRequest, TradingPlatform>
            _tpAddRequestToDbMapper;

        private readonly IMap<TradingPlatform, Models.Api.TradingPlatform> _tpDbToApiMapper;

        public TradingPlatformsController(ILogger<TradingPlatformsController> logger,
            StockerDbContext dbContext,
            IMap<TradingPlatform, Models.Api.TradingPlatform> tpDbToApiMapper,
            IMap<AddTradingPlatformRequest, TradingPlatform> tpAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _tpDbToApiMapper = tpDbToApiMapper;
            _tpAddRequestToDbMapper = tpAddRequestToDbMapper;
        }

        [HttpGet]
        public IEnumerable<Models.Api.TradingPlatform> Get([FromRoute] GetTradingPlatformsFilter filter)
        {
            var resultQuery = _dbContext.TradingPlatforms.Select(tp => tp);

            if (!string.IsNullOrWhiteSpace(filter?.Name))
                resultQuery = resultQuery.Where(tp =>
                    tp.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));

            foreach (var platform in resultQuery) yield return _tpDbToApiMapper.Map(platform);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddTradingPlatformRequest request)
        {
            var tradingPlatformNameLowercase = request.Name.ToLower();
            if (_dbContext.TradingPlatforms.Any(tp =>
                tp.Name == request.Name.ToLower()))
                return BadRequest("TradingPlatform already exists.");

            var tradingPlatform = _tpAddRequestToDbMapper.Map(request);
            await _dbContext.TradingPlatforms.AddAsync(tradingPlatform);
            await _dbContext.SaveChangesAsync();
            return Ok(new {tradingPlatform.Id});
        }
    }
}