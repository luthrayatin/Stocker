namespace Stocker.Models.Api
{
    public class Currency
    {
        public string Code { get; set; }
        public string Symbol { get; set; }
        public decimal MinorToMajorValueMultiplier { get; set; }
    }
}