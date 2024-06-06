using System;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class ProjectTaskById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public string ProjectTaskType { get; set; }
        public string ProjectTaskState { get; set; }
        public string ProjectName { get; set; }
        public string Priority {  get; set; }
        public string AssignedTo {  get; set; }
        public string PhotoPath {  get; set; }
    }
}
