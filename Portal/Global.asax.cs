using Autofac;
using Autofac.Integration.Web;
using MediatR;
using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using Portal.Infrastructure.AutofacModules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

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
            builder.RegisterType<Application.Commands.DefinationCurrencyCommand>();
            //builder.RegisterModule(new MediatorModule());
            // ... continue registering dependencies...

            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(builder.Build());
        }
    }
    //public class Global : System.Web.HttpApplication
    //{

    //    protected void Application_Start(object sender, EventArgs e)
    //    {
    //        var container = new ContainerBuilder();

    //        container.RegisterModule(new MediatorModule());

    //    }

    //    protected void Session_Start(object sender, EventArgs e)
    //    {

    //    }

    //    protected void Application_BeginRequest(object sender, EventArgs e)
    //    {

    //    }

    //    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    //    {

    //    }

    //    protected void Application_Error(object sender, EventArgs e)
    //    {

    //    }

    //    protected void Session_End(object sender, EventArgs e)
    //    {

    //    }

    //    protected void Application_End(object sender, EventArgs e)
    //    {

    //    }
    //}
}