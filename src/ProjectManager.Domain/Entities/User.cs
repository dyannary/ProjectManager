using ProjectManager.Domain.Common;
using ProjectManager.Domain.Entities;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class User : AuditableEntity
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public string PhotoPath { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<UserProjectTask> UserProjectTasks { get; set; }

        public virtual ICollection<NotificationEntity> Notifications { get; set; }

    }
}
