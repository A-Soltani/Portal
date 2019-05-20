using System;
using System.Collections.Generic;
using System.Linq;
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
            //register your profiles, or skip this if you don't want them in your container
            builder.RegisterAssemblyTypes().AssignableTo(typeof(Profile));

            //register your configuration as a single instance
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                //add your profiles (either resolve from container or however else you acquire them)
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            //register your mapper
            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();

        }
    }
}
