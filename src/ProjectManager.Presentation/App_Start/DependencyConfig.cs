using Autofac;
using Autofac.Integration.Mvc;
using ProjectManager.Application;
using ProjectManager.Infrastructure;
using System.Reflection;
using System.Web.Mvc;

namespace ProjectManager.Presentation
{
    public static class DependencyConfig
    {
        public static void RegisterDependencies()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            InfrastructureDependencyInjection.Register(builder);

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}