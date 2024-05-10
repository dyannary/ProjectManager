using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey(u => u.Id);
            Property(u => u.UserName).IsRequired().HasMaxLength(30);
            Property(u => u.Password).IsRequired().HasMaxLength(50);
            Property(u=>u.FirstName).IsRequired().HasMaxLength(50);

            Property(u => u.Email).IsRequired().HasMaxLength(50);
            Property(u => u.RoleId).IsRequired();
            Property(u => u.LastName).IsRequired().HasMaxLength(50);

            Property(u => u.LastName).IsRequired().HasMaxLength(50);
            Property(u => u.Email).IsRequired().HasMaxLength(50);
            Property(u => u.RoleId).IsRequired();

            Property(u => u.Created).IsOptional();
            Property(u => u.LastModified).IsOptional();

            HasMany(u => u.UserProject)
                .WithRequired(up => up.User)
                .HasForeignKey(up => up.UserId);

            HasMany(u => u.UserProjectTask)
                .WithRequired(upt => upt.User)
                .HasForeignKey(upt => upt.UserId);
        }
    }
}
