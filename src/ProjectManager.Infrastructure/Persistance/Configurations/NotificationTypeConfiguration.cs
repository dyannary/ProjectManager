using ProjectManager.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace ProjectManager.Infrastructure.Persistance.Configurations
{
    public class NotificationTypeConfiguration : EntityTypeConfiguration<NotificationType>
    {
        public NotificationTypeConfiguration() 
        {
            HasKey(n => n.Id);
            Property(n => n.Name).IsRequired().HasMaxLength(25);
            Property(n => n.Description).IsOptional();
            
            HasMany(N => N.Notifications)
                .WithRequired(n => n.Type)
                .HasForeignKey(n => n.TypeId);

        }
    }
}
