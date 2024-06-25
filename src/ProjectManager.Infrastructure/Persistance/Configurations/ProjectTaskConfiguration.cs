using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectTaskConfiguration : EntityTypeConfiguration<ProjectTask>
    {
        public ProjectTaskConfiguration() 
        {
            HasKey(pt=>pt.Id);
            Property(pt => pt.Name)
                .IsRequired()
                .HasMaxLength(100);
            Property(pt=>pt.Description)
                .IsOptional()
                .HasMaxLength(1000);
            Property(pt=>pt.TaskTypeId)
                .IsOptional();
            Property(pt=>pt.ProjectId)
                .IsRequired();

            HasMany(f => f.Files)
                .WithRequired(pt => pt.ProjectTask)
                .HasForeignKey(ft => ft.ProjectTaskId);

            HasMany(upt => upt.UserProjectTasks)
                .WithRequired(pt => pt.ProjectTask)
                .HasForeignKey(pti => pti.ProjectTaskId);
        }
    }
}
