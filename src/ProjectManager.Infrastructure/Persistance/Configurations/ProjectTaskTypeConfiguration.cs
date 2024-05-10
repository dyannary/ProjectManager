using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskTypeConfiguration : EntityTypeConfiguration<ProjectTaskType>
    {
        public ProjectTaskTypeConfiguration()
        {
            HasKey(pt => pt.Id);
            Property(pt => pt.Name);
            Property(pt => pt.Description);

            HasMany(p => p.ProjectTask)
                .WithRequired(pt=>pt.ProjectTaskType)
                .HasForeignKey(t=>t.TaskTypeId);
        }
    }
}
