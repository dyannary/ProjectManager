using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class UserProjectTaskConfiguration : EntityTypeConfiguration<UserProjectTask>
    {
        public UserProjectTaskConfiguration() 
        {
            HasKey(up => new { up.UserId, up.ProjectTaskId});
        }
    }
}
