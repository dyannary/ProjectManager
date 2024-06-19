using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using ProjectManager.Infrastructure.Persistance.Configurations;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Infrastructure.Persistance
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext() : base("name=ProjectManagerConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext, DatabaseInitializer>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserProjectTask> UserProjectTasks { get; set; }
        public DbSet<UserProjectRole> UserProjectRole { get; set; }
        public DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }
        public DbSet<ProjectTaskState> ProjectTaskStates { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectState> ProjectStates { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<FileType> FileTypes { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Configurations.Add(new RoleConfiguration());
            builder.Configurations.Add(new ProjectStateConfiguration());
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
            builder.Configurations.Add(new NotificationConfiguration());
            builder.Configurations.Add(new NotificationTypeConfiguration());
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public async Task<int> SaveAsync(CancellationToken cancellationToken)
        {
            return await this.SaveChangesAsync(cancellationToken); 
        }
        public override async Task<int> SaveChangesAsync()
        {
            return await this.SaveChangesAsync();
        }
    }
}