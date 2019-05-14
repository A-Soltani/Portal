using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

// Use the SimpleInjector.Integration.Web Nuget package
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Diagnostics;
using SimpleInjector.Integration.Web;

// Makes use of the System.ComponentModel.Composition assembly
using System.ComponentModel.Composition;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Commands;
using Autofac.Integration.Web;
using Autofac;
using Portal.Infrastructure.AutofacModules;
using Portal.Infrastructure.Repositories.DapperRepositories;
using Portal.Domain.AggregatesModel.CurrencyAggregate;

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

            builder.RegisterModule(new ApplicationModule());
            builder.RegisterModule(new MediatorModule());          

            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
}

