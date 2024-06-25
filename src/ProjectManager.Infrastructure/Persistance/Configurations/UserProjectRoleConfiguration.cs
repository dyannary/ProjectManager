using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class UserProjectRoleConfiguration : EntityTypeConfiguration<UserProjectRole>
    {
        public UserProjectRoleConfiguration() 
        {
            HasKey(upr => upr.Id);
            Property(upr => upr.Name)
                .HasMaxLength(50);
            Property(upr => upr.Description)
                .HasMaxLength(500);

            HasMany(up => up.UserProjects)
                .WithRequired(upr => upr.UserProjectRole)
                .HasForeignKey(upr => upr.ProjectRoleId);
        }
    }
}
