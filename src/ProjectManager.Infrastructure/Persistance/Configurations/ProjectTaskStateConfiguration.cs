using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskStateConfiguration : EntityTypeConfiguration<ProjectTaskState>
    {
        public ProjectTaskStateConfiguration()
        {
            HasKey(pts => pts.Id);
            Property(pts => pts.Name)
                .HasMaxLength(100);  
            Property(pts => pts.Description)
                .HasMaxLength(500);

            HasMany(pt => pt.ProjectTasks)
                .WithRequired(pts => pts.ProjectTaskState)
                .HasForeignKey(ts => ts.TaskStateId);
        }
    }
}
