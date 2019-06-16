using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Mappings.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper _mapper;
        public static void ConfigureAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            _mapper = config.CreateMapper();
            config.AssertConfigurationIsValid();
        }
    }
}
