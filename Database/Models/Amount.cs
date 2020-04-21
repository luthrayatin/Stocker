namespace Stocker.Database.Models
{
    public class Amount
    {
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public long ValueMinor { get; set; }
    }
}