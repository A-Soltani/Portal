using Autofac;
using Portal.Domain.AggregatesModel.CurrencyAggregate;
using Portal.Infrastructure.Repositories.DapperRepositories;


namespace Portal.Infrastructure.AutofacInfrastructureModules
{
    public class RepositoryModule : Autofac.Module
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
