using ProjectManager.Domain.Common;
using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class Project : AuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string PhotoPath { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectStartDate { get; set; }

        public int ProjectStateId { get; set; }
        public ProjectState ProjectState { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }

        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
