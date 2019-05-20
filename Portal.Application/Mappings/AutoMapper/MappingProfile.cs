using AutoMapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

namespace Portal.Application.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            //CreateMap<Currency, CurrencyDTO>();
            CreateMap<CurrencyDTO, Currency>()
                      .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Entity, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
            CreateMap<Test, Test>();
        }


    }
    public class Test
    {
        public int MyProperty { get; set; }
    }

    public class Test1
    {
        public int MyProperty { get; set; }
    }
}