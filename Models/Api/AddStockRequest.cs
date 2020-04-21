namespace Stocker.Models.Api
{
    public class AddStockRequest
    {
        public string Name { get; set; }
        public string Ticker { get; set; }
        public int StockExchangeId { get; set; }
    }
}