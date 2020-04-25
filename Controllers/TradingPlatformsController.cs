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
    public class TradingPlatformsController : ControllerBase
    {
        private readonly ILogger<TradingPlatformsController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMapper _mapper;
        public TradingPlatformsController(ILogger<TradingPlatformsController> logger,
        StockerDbContext dbContext,
        IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<TradingPlatform> Get([FromRoute]GetTradingPlatformsFilter filter)
        {
            var resultQuery = _dbContext.TradingPlatforms.Select(tp => tp);

            if (!string.IsNullOrWhiteSpace(filter?.Name))
            {
                resultQuery = resultQuery.Where(tp => tp.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }

            return _mapper.Map<IEnumerable<Database.Models.TradingPlatform>, IEnumerable<Models.Api.TradingPlatform>>(resultQuery);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody]AddTradingPlatformRequest request)
        {
            if (_dbContext.TradingPlatforms.Any(tp => tp.Name.Equals(request.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return BadRequest("TradingPlatform already exists.");
            }

            var tradingPlatform = _mapper.Map<AddTradingPlatformRequest, Database.Models.TradingPlatform>(request);
            _dbContext.TradingPlatforms.Add(tradingPlatform);
            await _dbContext.SaveChangesAsync();
            return Ok(new { Id = tradingPlatform.Id });
        }
    }
}