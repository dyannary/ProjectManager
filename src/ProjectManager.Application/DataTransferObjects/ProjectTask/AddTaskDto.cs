using System;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class AddTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public string TaskState { get; set; }
        public DateTime StartDate { get; set; }
    }
}
