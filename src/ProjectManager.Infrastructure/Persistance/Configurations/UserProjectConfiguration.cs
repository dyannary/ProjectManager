using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class UserProjectConfiguration : EntityTypeConfiguration<UserProject>
    {
        public UserProjectConfiguration() 
        {
            HasKey(up => new { up.UserId, up.ProjectId});
        }
    }
}
