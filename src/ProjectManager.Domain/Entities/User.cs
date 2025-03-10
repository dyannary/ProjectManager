﻿using ProjectManager.Domain.Common;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class User : AuditableEntity<int>
    {
        public string UserName {  get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }

        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<UserProject> UserProject { get; set; }

        public virtual ICollection<UserProjectTask> UserProjectTask { get; set; }
    }
}
