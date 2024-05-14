using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Threading;

namespace ProjectManager.Application.interfaces
{
    public interface IAppDbContext
    {
        DbSet<Domain.Entities.User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserProject> UserProjects { get; set; }
        DbSet<UserProjectTask> UserProjectTasks { get; set; }
        DbSet<UserProjectRole> UserProjectRole { get; set; }
        DbSet<ProjectTaskType> ProjectTaskTypes { get; set; }
        DbSet<ProjectTaskState> ProjectTaskStates { get; set; }
        DbSet<ProjectTask> ProjectTasks { get; set; }
        DbSet<ProjectState> ProjectStates { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<Priority> Priorities { get; set; }
        DbSet<FileType> FileTypes { get; set; }
        DbSet<File> Files { get; set; }

        void Save();
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}
