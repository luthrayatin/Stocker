namespace Stocker.Models.Api
{
    public class Stock
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public StockExchange StockExchange { get; set; }
    }
}