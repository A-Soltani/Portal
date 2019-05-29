using AutoMapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

namespace Portal.Application.Mappings.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<CurrencyDTO, Currency>()
                      .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Country, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
            CreateMap<Currency, CurrencyDTO>();
        }


    }
   
}