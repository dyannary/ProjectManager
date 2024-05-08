using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            this.HasKey(u => u.Id);
            this.Property(u => u.UserName).IsRequired().HasMaxLength(30);
            this.Property(u => u.Password).IsRequired().HasMaxLength(50);
            this.Property(u=>u.FirstName).IsRequired().HasMaxLength(50);
            this.Property(u => u.LastName).IsRequired().HasMaxLength(50);
            this.Property(u => u.Email).IsRequired().HasMaxLength(50);
            this.Property(u => u.RoleId).IsRequired();
        }
    }
}
