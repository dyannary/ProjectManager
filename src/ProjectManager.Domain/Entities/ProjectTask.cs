using ProjectManager.Domain.Common;
using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class ProjectTask : AuditableEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TaskEndDate { get; set; }
        public DateTime TaskStartDate { get; set; }


        public int TaskTypeId { get; set; }
        public virtual ProjectTaskType ProjectTaskType { get; set; }

        public int TaskStateId { get; set; }
        public virtual ProjectTaskState ProjectTaskStates { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project{ get; set; }

        public int PriorityID { get; set; }
        public virtual Priority Priority { get; set; }

        public virtual ICollection<File> File { get; set; }

        public virtual ICollection<UserProjectTask> UserProjectTask { get; set; }
    }
}
