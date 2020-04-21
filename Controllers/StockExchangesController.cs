using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocker.Models.Api;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockExchangesController : ControllerBase
    {
        private readonly ILogger<StockExchangesController> _logger;
        public StockExchangesController(ILogger<StockExchangesController> logger)
        {
            _logger = logger;
        }

        [HttpPost("[action]")]
        public IActionResult Add([FromBody]AddStockExchangeRequest request)
        {
            throw new NotImplementedException();
        }
    }
}