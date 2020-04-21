namespace Stocker.Models.Api
{
    public class AddStockExchangeRequest
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public int CurrencyId { get; set; }
    }
}