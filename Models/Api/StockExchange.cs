namespace Stocker.Models.Api
{
    public class StockExchange
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }
    }
}