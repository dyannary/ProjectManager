using Autofac;
using Autofac.Integration.SignalR;
using FluentValidation;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;
using System.Linq;
using System.Reflection;

namespace ProjectManager.Application.Extensions
{
    public class ApplicationDependencyInjection
    {
        public static void RegisterApplication(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordEncryptionService>().As<IPasswordEncryptionService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(ApplicationDependencyInjection).Assembly)
                .Where(t => t.GetInterfaces().Any(i => i.IsClosedTypeOf(typeof(IValidator<>))))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterHubs(Assembly.GetExecutingAssembly());
        }
    }
}
