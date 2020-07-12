using System;

namespace Stocker.Models.Api
{
    public class AddStockTransactionRequest
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public Amount PricePerUnit { get; set; }
        public decimal ConversionRate { get; set; }
        public Amount Commission { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public int StockExchangeId { get; set; }
        public int TradingPlatformId { get; set; }
        public int UserCurrencyId { get; set; }
    }
}