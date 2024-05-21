using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.DataSeeder.Seeds;
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

            Role adminRole = roles.Where(r => r.Name.Contains("admin")).First();
            Role userRole = roles.Where(r => r.Name.Contains("user")).First();

            var users = UsersSeed.SeedUsers(adminRole, userRole);

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }
    }
}
