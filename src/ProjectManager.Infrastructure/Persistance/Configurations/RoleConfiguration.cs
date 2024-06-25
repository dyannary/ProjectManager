using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration() 
        {
            HasKey(r => r.Id);
            Property(r => r.Name).IsRequired()
                .HasMaxLength(20);
            Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(100);

            HasMany(r => r.Users)
                .WithRequired(r => r.Role)
                .HasForeignKey(r => r.RoleId);
        }
    }
}
