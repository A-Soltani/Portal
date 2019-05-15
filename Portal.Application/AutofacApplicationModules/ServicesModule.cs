using Autofac;
using Portal.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.AutofacApplicationModules
{
    public class ServicesModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyService>()
                .As<ICurrencyService>()
                .InstancePerLifetimeScope();


        }
    }
}
