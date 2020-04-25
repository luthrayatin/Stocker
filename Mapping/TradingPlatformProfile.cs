using AutoMapper;

namespace Stocker.Mapping
{
    public class TradingPlatformProfile : Profile
    {
        public TradingPlatformProfile()
        {
            CreateMap<Database.Models.TradingPlatform, Models.Api.TradingPlatform>();
            CreateMap<Models.Api.AddTradingPlatformRequest, Database.Models.TradingPlatform>().ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}