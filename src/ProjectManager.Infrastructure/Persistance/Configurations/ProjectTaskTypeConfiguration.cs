using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

public class ProjectTaskTypeConfiguration : EntityTypeConfiguration<ProjectTaskType>
{
    public ProjectTaskTypeConfiguration()
    {
        HasKey(ptt => ptt.Id);
        Property(ptt => ptt.Name)
            .HasMaxLength(100);
        Property(ptt => ptt.Description)
            .HasMaxLength(500);

        HasMany(pt => pt.ProjectTasks)
            .WithRequired(ptt => ptt.ProjectTaskType)
            .HasForeignKey(pt => pt.TaskTypeId);
    }
}