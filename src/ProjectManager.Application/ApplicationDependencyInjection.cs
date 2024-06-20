using Autofac;
using Autofac.Integration.SignalR;
using FluentValidation;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.ProjectCollaborator.Commands;
using ProjectManager.Application.Projects.Commands.Create;
using ProjectManager.Application.Projects.Commands.Update;
using ProjectManager.Application.Services;
using ProjectManager.Application.User.Commands.LoginUser;
using ProjectManager.Application.UserManagement.Commands.UpdatePassword;
using System.Reflection;

namespace ProjectManager.Application
{
    public class ApplicationDependencyInjection
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordEncryptionService>().As<IPasswordEncryptionService>();
            builder.RegisterType<FileService>().As<IFileService>();
            builder.RegisterType<NotificationService>().As<INotificationService>().InstancePerLifetimeScope();

            builder.RegisterType<LoginUserCommandValidator>().As<IValidator<LoginUserDto>>();
            builder.RegisterType<CreateProjectCommandValidator>().As<IValidator<ProjectToCreateDto>>();
            builder.RegisterType<UpdateProjectCommandValidator>().As<IValidator<ProjectByIdDto>>();
            builder.RegisterType<CreateProjectCollaboratorCommandValidator>().As<IValidator<CollaboratorToCreateDto>>();
            builder.RegisterType<UpdateUserPasswordValidator>().As<IValidator<UserPasswordChangeDto>>();

            builder.RegisterHubs(Assembly.GetExecutingAssembly());

        }
    }
}
