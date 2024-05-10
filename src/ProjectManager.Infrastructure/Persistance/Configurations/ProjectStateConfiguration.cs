using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class ProjectStateConfiguration : EntityTypeConfiguration<ProjectState>
    {
        public ProjectStateConfiguration() 
        {
            HasKey(ps => ps.Id);

            Property(ps => ps.Name).IsRequired().HasMaxLength(30);
            Property(ps => ps.Description).IsRequired().HasMaxLength(80);

            HasMany(ps => ps.Projects)
                .WithRequired(p => p.ProjectState)
                .HasForeignKey(p => p.ProjectStateId);
        }
    }
}
