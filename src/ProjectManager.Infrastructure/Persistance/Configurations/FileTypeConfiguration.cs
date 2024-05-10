using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class FileTypeConfiguration : EntityTypeConfiguration<FileType>
    {
        public FileTypeConfiguration()
        {
            HasKey(ft => ft.Id);
            Property(t => t.Type).IsRequired();

            HasMany(f => f.File)
                .WithRequired(ft => ft.FileType)
                .HasForeignKey(ft => ft.FileTypeId);
        }
    }
}
