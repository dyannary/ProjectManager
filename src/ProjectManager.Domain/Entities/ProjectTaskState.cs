using ProjectManager.Domain.Common;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class ProjectTaskState : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ProjectTask> ProjectTask { get; set; }
    }
}
