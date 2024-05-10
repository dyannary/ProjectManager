using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class FileConfiguration : EntityTypeConfiguration<File>
    {
        public FileConfiguration()
        {
            HasKey(f => f.Id);

            Property(f => f.FileName).IsRequired().HasMaxLength(70);
            Property(f => f.FileData).IsRequired();
            Property(f => f.IsDeleted).IsRequired();
            Property(f => f.ProjectTaskId).IsOptional();
            Property(f => f.FileTypeId).IsOptional();

            Property(f => f.Created).IsOptional();
            Property(f => f.LastModified).IsOptional();

        }
    }
}