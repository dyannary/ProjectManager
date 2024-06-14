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
            Property(u => u.Password).IsRequired().HasMaxLength(150);
            Property(u=>u.FirstName).IsRequired().HasMaxLength(50);

            Property(u => u.Email).IsRequired().HasMaxLength(100);
            Property(u => u.RoleId).IsRequired();
            Property(u => u.LastName).IsRequired().HasMaxLength(50);

            Property(u => u.LastName).IsRequired().HasMaxLength(50);
            Property(u => u.Email).IsRequired().HasMaxLength(50);
            Property(u => u.RoleId).IsRequired();

            Property(u => u.Created).IsOptional();
            Property(u => u.LastModified).IsOptional();

            HasMany(u => u.UserProjects)
                .WithRequired(up => up.User)
                .HasForeignKey(up => up.UserId);

            HasMany(u => u.UserProjectTasks)
                .WithRequired(upt => upt.User)
                .HasForeignKey(upt => upt.UserId);

            HasMany(u => u.Notifications)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
