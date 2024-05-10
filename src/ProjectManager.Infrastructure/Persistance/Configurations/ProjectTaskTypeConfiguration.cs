using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskTypeConfiguration : EntityTypeConfiguration<ProjectTaskType>
    {
        public ProjectTaskTypeConfiguration()
        {
            HasKey(ptt => ptt.Id);
            Property(ptt => ptt.Name);
            Property(ptt => ptt.Description);

            HasMany(pt=>pt.ProjectTask)
                .WithRequired(ptt=>ptt.ProjectTaskType)
                .HasForeignKey(pt=>pt.TaskTypeId);
        }
    }
}
