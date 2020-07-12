namespace Stocker.Models.Api
{
    public class StockExchange
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }
    }
}