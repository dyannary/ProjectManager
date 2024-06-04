using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.DataSeeder.Seeds;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net.Mime;

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
                ProjectSeed(context);

            if (!context.ProjectTasks.Any())
                ProjectTasksSeed(context);  

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

        private void ProjectSeed(AppDbContext context)
        {
            var projects = new List<Project>()
            {
                new Project
                {
                    Id = 1,
                    Name = "First Project",
                    Description = "First Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 6, 2),
                    ProjectEndDate = new DateTime(2024, 7, 3),
                    ProjectStateId = 1,
                    UserProjects = new List<UserProject>(), // Assuming some default values or method to generate
                    ProjectTasks = new List<ProjectTask>() // Assuming some default values or method to generate
                },
                new Project
                {
                    Id = 2,
                    Name = "Second Project",
                    Description = "Second Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 5, 2),
                    ProjectEndDate = new DateTime(2024, 8, 12),
                    ProjectStateId = 2,
                    UserProjects = new List<UserProject>(),
                    ProjectTasks = new List<ProjectTask>()
                },
                new Project
                {
                    Id = 3,
                    Name = "Third Project",
                    Description = "Third Description",
                    IsDeleted = false,
                    PhotoPath = $"~/Content/Images/Default/defaultImage.jpg",
                    ProjectStartDate = new DateTime(2024, 7, 1),
                    ProjectEndDate = new DateTime(2024, 9, 12),
                    ProjectStateId = 1,
                    UserProjects = new List<UserProject>(),
                    ProjectTasks = new List<ProjectTask>()
                }
            };

            projects.ForEach(t => context.Projects.Add(t));
            context.SaveChanges();
        }

        private void ProjectTasksSeed(AppDbContext context)
        {
            var tasks = new List<ProjectTask>()
            {
                new ProjectTask()
                {
                    Name = "Add project task seed",
                    Description = "This is a task",
                    TaskStartDate = new DateTime(2024, 8, 12),
                    TaskEndDate = new DateTime(2024, 9, 12),
                    PriorityId = 1, // Assuming there are 4 priority levels
                    TaskTypeId = 1, // Assuming there are 4 task types
                    TaskStateId = 1, // Assuming there are 4 task states
                    ProjectId = 1, // Assuming there are 4 projects
                    Files = new List<File>(),
                    UserProjectTasks = new List<UserProjectTask>()
                },
            };

            tasks.ForEach(t => context.ProjectTasks.Add(t));
            context.SaveChanges();
        }
    }
}
