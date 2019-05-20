using AutoMapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

namespace Portal.Application.Mappings.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Currency, CurrencyDTO>();
        }

    }
}