using System;

namespace Stocker.Models.Api
{
    public class GetStocksFilter
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? FromTransactionDate { get; set; }
        public DateTimeOffset? ToTransactionDate { get; set; }
        public bool GetClosedPositions { get; set; }
    }
}