using ProjectManager.Domain.Common;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}