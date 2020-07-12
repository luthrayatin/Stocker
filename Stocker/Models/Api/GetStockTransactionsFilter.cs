using System;

namespace Stocker.Models.Api
{
    public class GetStockTransactionsFilter
    {
        public DateTimeOffset? FromTransactionDate { get; set; }
        public DateTimeOffset? ToTransactionDate { get; set; }
        public bool GetClosedPositions { get; set; }
    }
}