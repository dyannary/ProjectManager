using System;
using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public int TaskTypeId { get; set; }
        public int TaskStateId { get; set; }
        public int PriorityId { get; set; }
        public DateTime StartDate { get; set; }
        public List<string> PhotoPaths {  get; set; } 
    }
}
