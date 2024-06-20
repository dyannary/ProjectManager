using System;
using System.Collections.Generic;
using System.Web;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class AddTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AssignedTo { get; set; }
        public int TaskTypeId { get; set; }
        public int TaskStateId { get; set; }
        public int PriorityId { get; set; }
        public int ProjectId { get; set; }
        public DateTime StartDate { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
    }
}
