using Autofac;
using ProjectManager.Application.interfaces;
using ProjectManager.Infrastructure.Persistance;

namespace ProjectManager.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().As<IAppDbContext>();
        }
    }
}
