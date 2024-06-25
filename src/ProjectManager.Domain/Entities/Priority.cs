﻿using ProjectManager.Domain.Common;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class Priority : Entity
    {
        public string Name { get; set; }
        public int PriorityValue { get; set; }
        public virtual ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
