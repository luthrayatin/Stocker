using AutoMapper;

namespace Stocker.Mapping
{
    public class StockProfile : Profile
    {
        public StockProfile()
        {
            CreateMap<Database.Models.Stock, Models.Api.Stock>();
            CreateMap<Models.Api.AddStockRequest, Database.Models.Stock>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.StockExchange, opt => opt.Ignore());
        }
    }
}