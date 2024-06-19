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

            if (!context.ProjectTaskStates.Any())
                CreateProjectTaskStates(context);

            if (!context.ProjectTaskTypes.Any())
                CreateProjectTaskTypes(context);

            if (!context.ProjectStates.Any())
                CreateProjectStates(context);

            if (!context.Priorities.Any())
                CreatePriotities(context);

            if (!context.Projects.Any())
                CreateProjects(context);

            if (!context.ProjectTasks.Any())
                CreateProjectTasks(context);

            if (!context.NotificationTypes.Any())
                CreateNotificationTypes(context);

            base.Seed(context);
        }

        private void CreateNotificationTypes(AppDbContext context)
        {
            var types = new List<NotificationType>()
            {
                new NotificationType {Name = "Project Collaborators", Description = "Notification type for collaborators"},
                new NotificationType {Name = "Task", Description = "Notification type for tasks"},
            };
            types.ForEach(type => context.NotificationTypes.Add(type));
            context.SaveChanges();
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

            var users = UsersSeed.Seed(adminRole, userRole);

            users.ForEach(user => context.Users.Add(user));
            context.SaveChanges();
        }

        private void CreateProjectTaskStates(AppDbContext context)
        {
            var projectTaskStates = new List<ProjectTaskState>()
            {
                new ProjectTaskState {Name = "Done", Description = "Project is done"},
                new ProjectTaskState {Name = "Pending", Description = "Project is Pending"},
                new ProjectTaskState {Name = "Rejected", Description = "Project is Rejected"},
                new ProjectTaskState {Name = "Confirmed", Description = "Project is Confirmed"},
                new ProjectTaskState {Name = "In Progress", Description = "Project is in Progress"},
            };

            projectTaskStates.ForEach(pts => context.ProjectTaskStates.Add(pts));
            context.SaveChanges();
        }

        private void CreateProjectTaskTypes(AppDbContext context)
        {
            var projectTaskType = new List<ProjectTaskType>()
            {
                new ProjectTaskType {Name = "Bug", Description = "The type of this project is ,,Bug,,"},
                new ProjectTaskType {Name = "Feature", Description = "The type of this project is ,,Feature,,"},
                new ProjectTaskType {Name = "Modify", Description = "The type of this project is ,,Modify,,"},
            };

            projectTaskType.ForEach(ptt => context.ProjectTaskTypes.Add(ptt));
            context.SaveChanges();
        }

        private void CreateProjectStates(AppDbContext context)
        {
            var projectStates = new List<ProjectState>()
            {
                new ProjectState {Name = "Development", Description = "The state for this project is in development"},
                new ProjectState {Name = "Maintenance", Description = "The state for this project is in maintenance"},
                new ProjectState {Name = "Frozen", Description = "The state for this project is frozen"},
                new ProjectState {Name = "Done", Description = "The state for this project is done"},
            };

            projectStates.ForEach(ps => context.ProjectStates.Add(ps));
            context.SaveChanges();
        }

        private void CreatePriotities(AppDbContext context)
        {
            var priorities = new List<Priority>()
            {
                new Priority {Name = "Low", PriorityValue = 1},
                new Priority {Name = "Medium", PriorityValue = 2},
                new Priority {Name = "High", PriorityValue = 3},
                new Priority {Name = "Urgent", PriorityValue = 4},
            };

            priorities.ForEach(p => context.Priorities.Add(p));
            context.SaveChanges();
        }

        private void CreateProjects(AppDbContext context)
        {
            var projects = ProjectsSeed.Seed();

            projects.ForEach(p => context.Projects.Add(p));

            context.SaveChanges();
        }

        private void CreateProjectTasks(AppDbContext context)
        {
            var tasks = ProjectTasksSeed.Seed();

            tasks.ForEach(pt => context.ProjectTasks.Add(pt));

            context.SaveChanges();
        }
    }
}
