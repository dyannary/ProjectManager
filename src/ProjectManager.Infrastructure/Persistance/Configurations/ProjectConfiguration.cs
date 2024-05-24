using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration() 
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Description).IsRequired().HasMaxLength(50);
            Property(p => p.IsDeleted).IsRequired();
            Property(p => p.ProjectEndDate).IsRequired();
            Property(p => p.ProjectStartDate).IsRequired();
            Property(p => p.PhotoPath).IsOptional().HasMaxLength(150);

            Property(p => p.Created).IsOptional();
            Property(p => p.LastModified).IsOptional();

            HasMany(p => p.UserProjects)
                .WithRequired(up => up.Project)
                .HasForeignKey(up => up.ProjectId);

            HasMany(p => p.ProjectTasks)
                .WithRequired(pt => pt.Project)
                .HasForeignKey(pt => pt.ProjectId);
        }
    }
}
