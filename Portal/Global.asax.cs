using System;
using System.Web;
using Autofac.Integration.Web;
using Autofac;
using Portal.Application.AutofacApplicationModules;
using Portal.Infrastructure.AutofacInfrastructureModules;

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

            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
}

