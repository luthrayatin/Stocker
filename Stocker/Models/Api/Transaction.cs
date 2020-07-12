using System;

namespace Stocker.Models.Api
{
    public class Transaction
    {
        public int StockId { get; set; }
        public DateTimeOffset Date { get; set; }
        public string TradingPlatform { get; set; }
        public int Quantity { get; set; }
        public Amount PricePerUnit { get; set; }
        public Amount PricePerUnitUser { get; set; }
        public DateTimeOffset LoggedAt { get; set; }
        public Amount CommissionUser { get; set; }
    }
}