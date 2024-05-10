using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistance.Configurations;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Persistance
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserProject> UserProject { get; set; }
        public DbSet<UserProjectTask> UserProjectTasks { get; set; }
        public DbSet<ProjectTaskType> ProjectTaskType { get; set; }
        public DbSet<ProjectTaskState> ProjectTaskState { get; set; }
        public DbSet<ProjectTask> ProjectTask { get; set; }
        public DbSet<ProjectState> ProjectState { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Priority> Priority { get; set; }
        public DbSet<FileType> FileType { get; set; }
        public DbSet<File> File { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Configurations.Add(new RoleConfiguration());
            builder.Configurations.Add(new ProjectStateConfiguration());
            builder.Configurations.Add(new RoleConfiguration());
            builder.Configurations.Add(new UserConfiguration());
            builder.Configurations.Add(new UserProjectConfiguration());
            builder.Configurations.Add(new UserProjectTaskConfiguration());
            builder.Configurations.Add(new ProjectConfiguration());
            builder.Configurations.Add(new ProjectTaskConfiguration());
            builder.Configurations.Add(new ProjectTaskStateConfiguration());
            builder.Configurations.Add(new ProjectTaskTypeConfiguration());
            builder.Configurations.Add(new PriorityConfiguration());
            builder.Configurations.Add(new FileConfiguration());
            builder.Configurations.Add(new FileTypeConfiguration());
        }
    }
}
