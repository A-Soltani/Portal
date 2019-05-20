using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using Portal.Application.Mappings.AutoMapper;

namespace Portal.Application.AutofacApplicationModules
{
    public class AutoMapperModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            //builder.RegisterAssemblyTypes(typeof(IMapper).GetTypeInfo().Assembly).AsImplementedInterfaces();
        }
    }
}
