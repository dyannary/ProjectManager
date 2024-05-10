using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class FileConfiguration : EntityTypeConfiguration<File>

    {
        public FileConfiguration() 
        {
            HasKey(f => f.Id);
            Property(f=>f.FileName).IsRequired();
            Property(f => f.FileData).IsRequired();
            Property(f => f.IsDeleted).IsRequired();
            Property(f => f.ProjectTaskId).IsRequired();
            Property(f => f.Created).IsRequired();
            Property(f => f.LastModified).IsOptional();
        }
    }
}
