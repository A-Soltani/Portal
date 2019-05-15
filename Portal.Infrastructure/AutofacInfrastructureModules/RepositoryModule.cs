using Autofac;
using AutoMapper;
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

            //builder.RegisterType<Mapper>()
            //               .As<IMapper>()
            //               .InstancePerLifetimeScope();

            //        builder.Register(
            //            ctx =>
            //            {
            //                var scope = ctx.Resolve<ILifetimeScope>();
            //                    return new Mapper(ctx.Resolve<IConfigurationProvider>(),scope.Resolve);
            //            })
            //            .As<IMapper>()
            //            .InstancePerLifetimeScope();
        }
    }
}
