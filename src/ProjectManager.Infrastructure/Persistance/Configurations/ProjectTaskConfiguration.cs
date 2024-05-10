using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskConfiguration : EntityTypeConfiguration<ProjectTask>
    {
        public ProjectTaskConfiguration() 
        {
            HasKey(pt=>pt.Id);
            Property(pt => pt.Name).IsRequired();
            Property(pt=>pt.Description).IsOptional();
            Property(pt=>pt.TaskTypeId).IsOptional();
            Property(pt=>pt.ProjectId).IsRequired();

            HasMany(f => f.Files)
                .WithRequired(pt => pt.ProjectTask)
                .HasForeignKey(ft => ft.FileTypeId);

            HasMany(upt => upt.UserProjectTasks)
                .WithRequired(pt => pt.ProjectTask)
                .HasForeignKey(pti => pti.ProjectTaskId);
        }
    }
}
