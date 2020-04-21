using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stocker.Models.Api;

namespace Stocker.Controllers
{
    [ApiController]
    [Route("{stockId}")]
    public class StockTransactionsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<StockTransactionsController> _logger;

        public StockTransactionsController(ILogger<StockTransactionsController> logger)
        {
            _logger = logger;
            _logger.LogInformation("TransactionsController instantiated.");
        }

        [HttpGet("[action]")]
        public IEnumerable<Transaction> GetTransactions()
        {
            _logger.LogInformation("TransactionsController: Call reached GET method.");
            throw new NotImplementedException();
        }

        [HttpPost("[action]")]
        public Task<IActionResult> AddTransaction([FromBody]AddStockTransactionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}