using System;
using System.Web;
using Autofac.Integration.Web;
using Autofac;
using Portal.Application.AutofacApplicationModules;
using Portal.Infrastructure.AutofacInfrastructureModules;
using AutoMapper;
using System.Collections.Generic;

namespace Portal
{
    public class Global : HttpApplication, Autofac.Integration.Web.IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        IContainerProvider IContainerProviderAccessor.ContainerProvider => _containerProvider;

        protected void Application_Start(object sender, EventArgs e)
        {
            // Build up your application container and register your dependencies.
            var builder = new ContainerBuilder();

            builder.RegisterModule(new MediatorModule());
            builder.RegisterModule(new RepositoryModule());



            //builder.RegisterModule(new AutoMapperModule());
            //builder.RegisterModule(new ServicesModule());

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



            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
}

