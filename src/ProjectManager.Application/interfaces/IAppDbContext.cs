using ProjectManager.Domain.Entities;
using System.Data.Entity;

namespace ProjectManager.Application.interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> User { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<UserProject> UserProject { get; set; }
        DbSet<UserProjectTask> UserProjectTasks { get; set; }
        DbSet<ProjectTaskType> ProjectTaskType { get; set; }
        DbSet<ProjectTaskState> ProjectTaskState { get; set; }
        DbSet<ProjectTask> ProjectTask { get; set; }
        DbSet<ProjectState> ProjectState { get; set; }
        DbSet<Project> Project { get; set; }
        DbSet<Priority> Priority { get; set; }
        DbSet<FileType> FileType { get; set; }
        DbSet<File> File { get; set; }
    }
}
