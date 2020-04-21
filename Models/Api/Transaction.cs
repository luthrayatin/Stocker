using System;

namespace Stocker.Models.Api
{
    public class Transaction
    {
        public DateTimeOffset Date { get; set; }
        public string TradingPlatform { get; set; }
        public Amount Price { get; set; }
        public Amount PriceUser { get; set; }
        public DateTimeOffset LoggedAt { get; set; }
        public Amount Commission { get; set; }
        public Amount CommissionUser { get; set; }
    }
}