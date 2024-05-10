using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class PriorityConfiguration : EntityTypeConfiguration<Priority>
    {
        public PriorityConfiguration() 
        {
            HasKey(p => p.Id);
            Property(pt => pt.Name).IsRequired();
            Property(pt => pt.PriorityValue).IsOptional();

            HasMany(pt => pt.ProjectTask)
                .WithRequired(p => p.Priority)
                .HasForeignKey(p => p.PriorityId);
        }
    }
}
