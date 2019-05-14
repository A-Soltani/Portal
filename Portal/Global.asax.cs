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

[assembly: PreApplicationStartMethod(
    typeof(Portal.PageInitializerModule),
    "Initialize")]

namespace Portal
{
    public sealed class PageInitializerModule : IHttpModule
    {
        public static void Initialize()
        {
            DynamicModuleUtility.RegisterModule(typeof(PageInitializerModule));
        }

        void IHttpModule.Init(HttpApplication app)
        {
            app.PreRequestHandlerExecute += (sender, e) =>
            {
                var handler = app.Context.CurrentHandler;
                if (handler != null)
                {
                    string name = handler.GetType().Assembly.FullName;
                    if (!name.StartsWith("System.Web") &&
                        !name.StartsWith("Microsoft"))
                    {
                        Global.InitializeHandler(handler);
                    }
                }
            };
        }

        void IHttpModule.Dispose() { }
    }

    public class Global : HttpApplication
    {
        private static Container container;

        public static void InitializeHandler(IHttpHandler handler)
        {
            Type handlerType = handler is Page
                    ? handler.GetType().BaseType
                    : handler.GetType();
            container.GetRegistration(handlerType, true).Registration
                .InitializeInstance(handler);
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            Mediator();
        }

        private static void Mediator()
        {
            // 1. Create a new Simple Injector container.
            var container = new Container();
            var assemblies = GetAssemblies().ToArray();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register a custom PropertySelectionBehavior to enable property injection.
            container.Options.PropertySelectionBehavior =
                new ImportAttributePropertySelectionBehavior();

            // Register your Page classes to allow them to be verified and diagnosed.
            RegisterWebPages(container);

            // 3. Store the container for use by Page classes.
            Global.container = container;

            container.RegisterSingleton<IMediator, Mediator>();
            //container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Collection.Register(typeof(IPipelineBehavior<,>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestPreProcessor<>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestPostProcessor<,>), Enumerable.Empty<Type>());
            container.Collection.Register(typeof(IRequestHandler<,>), assemblies);
            //container.Register(typeof(IRequest), typeof(DefinationCurrencyCommand));
            //container.Register(typeof(IRequestHandler<,>), typeof(DefinationCurrencyCommandHandler<>.Handler));

            //container.Register(typeof(DefinationCurrencyCommand) , typeof(IRequestHandler<,>));

            //container.Register(typeof(IRequestHandler<,>), typeof(DefinationCurrencyCommand));


            container.Register(() => new ServiceFactory(container.GetInstance), Lifestyle.Singleton);

            container.Verify();

        }



        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(IMediator).GetTypeInfo().Assembly;
        }

        private static void RegisterWebPages(Container container)
        {
            var pageTypes =
                from assembly in BuildManager.GetReferencedAssemblies().Cast<Assembly>()
                where !assembly.IsDynamic
                where !assembly.GlobalAssemblyCache
                from type in assembly.GetExportedTypes()
                where type.IsSubclassOf(typeof(Page))
                where !type.IsAbstract && !type.IsGenericType
                select type;

            foreach (Type type in pageTypes)
            {
                var reg = Lifestyle.Transient.CreateRegistration(type, container);
                reg.SuppressDiagnosticWarning(
                    DiagnosticType.DisposableTransientComponent,
                    "ASP.NET creates and disposes page classes for us.");
                container.AddRegistration(type, reg);
            }
        }

        class ImportAttributePropertySelectionBehavior : IPropertySelectionBehavior
        {
            public bool SelectProperty(Type implementationType, PropertyInfo property)
            {
                // Makes use of the System.ComponentModel.Composition assembly
                return typeof(Page).IsAssignableFrom(implementationType) &&
                    property.GetCustomAttributes(typeof(ImportAttribute), true).Any();
            }
        }
    }

}