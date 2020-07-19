using Mapping;
using Stocker.Models.Api;
using TradingPlatform = Stocker.Database.Models.TradingPlatform;

namespace Stocker.Mapping
{
    public class TradingPlatformDbToApiMap : IMap<TradingPlatform, Models.Api.TradingPlatform>
    {
        public Models.Api.TradingPlatform Map(TradingPlatform source)
        {
            return new Models.Api.TradingPlatform
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }

    public class
        TradingPlatformAddRequestToDbMap : IMap<AddTradingPlatformRequest, TradingPlatform>
    {
        public TradingPlatform Map(AddTradingPlatformRequest source)
        {
            return new TradingPlatform
            {
                Name = source.Name
            };
        }
    }
}