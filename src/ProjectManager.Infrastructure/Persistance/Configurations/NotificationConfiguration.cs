using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class NotificationConfiguration : EntityTypeConfiguration<NotificationEntity>
    {
        public NotificationConfiguration() 
        {
            HasKey(n => n.Id);
            Property(n => n.Message).IsRequired().HasMaxLength(100);
            Property(n => n.Created).IsOptional();
        }
    }
}
