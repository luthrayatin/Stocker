namespace Stocker.Models.Api
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal MinorToMajorMultiplier { get; set; }
    }
}