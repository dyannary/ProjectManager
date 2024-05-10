using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class FileTypeConfiguration : EntityTypeConfiguration<FileType>
    {
        public FileTypeConfiguration() 
        {
            HasKey(ft => ft.Id);

            Property(ft => ft.Type).IsRequired().HasMaxLength(30);

            HasMany(ft => ft.File)
                .WithRequired(f => f.FileType)
                .HasForeignKey(f => f.FileTypeId);
        }
    }
}
