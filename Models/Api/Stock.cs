using System.Collections.Generic;

namespace Stocker.Models.Api
{
    public class Stock
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public int QuantityHeld { get; set; }
        public Amount AveragePurchaseAmount { get; set; }
        public Amount AveragePurchaseAmountUser { get; set; }
        public string StockExchange { get; set; }
        public string TradingPlatform { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}