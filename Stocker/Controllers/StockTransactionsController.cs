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
    [Route("stock/{stockId}/transactions")]
    public class StockTransactionsController : ControllerBase
    {
        private readonly ILogger<StockTransactionsController> _logger;
        private readonly StockerDbContext _dbContext;
        private readonly IMap<Database.Models.StockTransaction, Transaction> _transactionDbToApiMapper;

        private readonly IMap<AddStockTransactionRequest, Database.Models.StockTransaction>
            _transactionAddRequestToDbMapper;

        public StockTransactionsController(ILogger<StockTransactionsController> logger,
            StockerDbContext dbContext, IMap<Database.Models.StockTransaction, Transaction> transactionDbToApiMapper,
            IMap<AddStockTransactionRequest, Database.Models.StockTransaction> transactionAddRequestToDbMapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _transactionDbToApiMapper = transactionDbToApiMapper;
            _transactionAddRequestToDbMapper = transactionAddRequestToDbMapper;
            _logger.LogInformation("TransactionsController instantiated.");
        }

        /// <summary>
        /// Get all transactions for user based on the token.
        /// </summary>
        /// <returns>All transactions for the user.</returns>
        [HttpGet]
        public IEnumerable<Transaction> Get()
        {
            _logger.LogInformation("TransactionsController: Call reached GET method.");
            //TODO: Get UserId from Token once Authentication is implemented.
            foreach (var stockTransaction in _dbContext.StockTransactions.Where(st => st.UserId == 1)
                .OrderByDescending(st => st.Id))
            {
                yield return _transactionDbToApiMapper.Map(stockTransaction);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] AddStockTransactionRequest request)
        {
            _logger.LogInformation("Processing AddStockTransactionRequest: {@Request}", request);
            var transaction = _transactionAddRequestToDbMapper.Map(request);
            await _dbContext.StockTransactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), null);
        }
    }
}