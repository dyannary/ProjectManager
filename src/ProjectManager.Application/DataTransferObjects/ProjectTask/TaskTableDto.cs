﻿using System;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class TaskTableDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AssignedTo { get; set; }
        public int Priority {  get; set; }
        public string TaskState {  get; set; }
        public DateTime StartDate { get; set; }
    }
}
