using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin.Security;
using ProjectManager.Infrastructure;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using MediatR;
using ProjectManager.Application.User.Queries;
using ProjectManager.Application;

namespace ProjectManager.Presentation
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {

            var builder = new ContainerBuilder();

            builder.RegisterType<Mediator>().As<IMediator>().InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => componentContext.Resolve(t);
            });


            builder.Register(a => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(ApplicationDependencyInjection).Assembly)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>))
                   .InstancePerLifetimeScope();

            InfrastructureDependencyInjection.Register(builder);
            ApplicationDependencyInjection.Register(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}