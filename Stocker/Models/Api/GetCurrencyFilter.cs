using Microsoft.AspNetCore.Mvc;

namespace Stocker.Models.Api
{
    public class GetCurrencyFilter
    {
        [FromRoute]
        public string Name { get; set; }
        [FromRoute]
        public string Code { get; set; }
    }
}