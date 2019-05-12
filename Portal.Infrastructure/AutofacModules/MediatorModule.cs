using Autofac;
using MediatR;
using Portal.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Infrastructure.AutofacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(DefinationCurrencyCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));            


            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            //builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            //builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

        }
    }
}
