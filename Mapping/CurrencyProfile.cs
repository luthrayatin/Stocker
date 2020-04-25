using AutoMapper;
using Stocker.Models.Api;

namespace Stocker.Mapping
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Database.Models.Currency, Currency>();
            CreateMap<AddCurrencyRequest, Database.Models.Currency>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}