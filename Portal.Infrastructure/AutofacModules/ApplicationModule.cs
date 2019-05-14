using Autofac;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Infrastructure.Repositories.DapperRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.AutofacModules
{
    public class ApplicationModule
        : Autofac.Module
    {

        public string QueriesConnectionString { get; }

        //public ApplicationModule(string qconstr)
        //{
        //    QueriesConnectionString = qconstr;
        //}      

        protected override void Load(Autofac.ContainerBuilder builder)
        {

            builder.RegisterType<DapperCurrencyRepository>()
                .As<ICurrencyRepository>()
                .InstancePerLifetimeScope();

        }
    }
}
