﻿using Autofac;
using FluentValidation;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Services;

namespace ProjectManager.Application
{
    public class ApplicationDependencyInjection
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<PasswordEncryptionService>().As<IPasswordEncryptionService>();
            builder.RegisterType<FileService>().As<IFileService>();

        }
    }
}
