using AutoMapper;
using Stocker.Models.Api;

namespace Stocker.Mapping
{
    public class StockExchangeProfile : Profile
    {
        public StockExchangeProfile()
        {
            CreateMap<AddStockExchangeRequest, Database.Models.StockExchange>()
            .ForMember(se => se.Currency, opt => opt.Ignore())
            .ReverseMap();
        }
    }
}