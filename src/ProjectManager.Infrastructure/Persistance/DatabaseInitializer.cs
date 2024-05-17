using ProjectManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace ProjectManager.Infrastructure.Persistance
{
    public class DatabaseInitializer : DbMigrationsConfiguration<AppDbContext>
    {

        public DatabaseInitializer() 
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AppDbContext context)
        {
            if (!context.Roles.Any())
                CreateRolesForUser(context);

            if (!context.Users.Any())
                CreateUsers(context);

            if (!context.UserProjectRole.Any())
                CreateRolesForUserProject(context);

            base.Seed(context);
        }

        private void CreateRolesForUser(AppDbContext context)
        {
            var rolesUser = new List<Role>()
            {
                new Role { Name = "admin", Description = "Administrator with full access and privileges." },
                new Role { Name = "user", Description = "Standard user with basic access and privileges." }
            };
            rolesUser.ForEach(role => context.Roles.Add(role));
            context.SaveChanges();
        }

        private void CreateRolesForUserProject(AppDbContext context)
        {
            var rolesUserProject = new List<UserProjectRole>()
            {
                new UserProjectRole { Name = "ProjectCreator", Description = "Can create projects and has full control over them." },
                new UserProjectRole { Name = "ProjectAdministrator", Description = "Has administrative privileges over projects, including management and configuration." },
                new UserProjectRole { Name = "User", Description = "Standard user with basic access to projects and limited privileges." }
            };

            rolesUserProject.ForEach(role => context.UserProjectRole.Add(role));
            context.SaveChanges();
        }

        private void CreateUsers(AppDbContext context)
        {
            var roles = context.Roles.ToList();

            Role AdminRole = roles.Where(r => r.Name.Contains("admin")).First();
            Role UserRole = roles.Where(r => r.Name.Contains("user")).First();

            var users = new List<User>()
            {
                new User {UserName = "admin", Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918", FirstName = "admin", LastName = "admin"
                , Email = "sergiu.sorocean@iongroup.com", IsEnabled = true, Role = AdminRole },
                new User {UserName = "user", Password="04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb", FirstName = "user", LastName = "user",
                Email = "sergiu.sorocean@iongroup.com", IsEnabled = true, Role = UserRole}
            };

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }
    }
}
