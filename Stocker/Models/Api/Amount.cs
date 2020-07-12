namespace Stocker.Models.Api
{
    public class Amount
    {
        public decimal ValueMinor { get; set; }
        public string CurrencyCode { get; set; }
        public int CurrencyId { get; set; }
    }
}