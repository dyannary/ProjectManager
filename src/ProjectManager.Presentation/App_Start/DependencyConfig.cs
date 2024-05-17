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
using ProjectManager.Application.Projects.Queries;

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

            InfrastructureDependencyInjection.Register(builder);
            ApplicationDependencyInjection.Register(builder);

            builder.RegisterType<GetUserByUsernameAndPasswordHandler>().AsImplementedInterfaces();
            builder.RegisterType<GetProjectsByFilterHandler>().AsImplementedInterfaces();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}