using AutoMapper;
using Portal.Application.ModelDTOs;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                //Complex to Flattened
                cfg.CreateMap<CurrencyDTO, Currency>()
                    .ConstructUsing(cd => Currency.CurrencyDefinition(cd.CurrencyNumericCode, cd.Entity, cd.CurrencyType, cd.AlphabeticCode, cd.ExchangeRate, cd.UserID));
            });


        }

    }
}
