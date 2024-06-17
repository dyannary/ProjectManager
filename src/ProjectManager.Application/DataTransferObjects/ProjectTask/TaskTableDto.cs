using System;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class TaskTableDto
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
        public string AssignedTo { get; set; }
        public string ProjectTaskType {  get; set; }
        public string ProjectTaskState { get; set; }
        public string Priority { get; set; }
        public string PhotoPath { get; set; }
    }
}
