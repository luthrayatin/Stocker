namespace Stocker.Models.Api
{
    public class AddCurrencyRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public decimal MinorToMajorMultiplier { get; set; }
    }
}