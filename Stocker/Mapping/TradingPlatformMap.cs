using Mapping;
using Stocker.Models.Api;

namespace Stocker.Mapping
{
    public class TradingPlatformDbToApiMap : IMap<Database.Models.TradingPlatform, Models.Api.TradingPlatform>
    {
        public Models.Api.TradingPlatform Map(Database.Models.TradingPlatform source)
        {
            return new Models.Api.TradingPlatform
            {
                Name = source.Name
            };
        }
    }

    public class TradingPlatformAddRequestToDbMap : IMap<Models.Api.AddTradingPlatformRequest, Database.Models.TradingPlatform>
    {
        public Database.Models.TradingPlatform Map(AddTradingPlatformRequest source)
        {
            return new Database.Models.TradingPlatform
            {
                Name = source.Name
            };
        }
    }
}