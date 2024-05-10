using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskStateConfiguration : EntityTypeConfiguration<ProjectTaskState>
    {
        public ProjectTaskStateConfiguration()
        {
            HasKey(pts => pts.Id);
            Property(pts => pts.Name);  
            Property(pts => pts.Description);

            HasMany(pt => pt.ProjectTask)
                .WithRequired(pts => pts.ProjectTaskState)
                .HasForeignKey(ts => ts.TaskStateId);
        }
    }
}
