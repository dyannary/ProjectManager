using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class ProjectTaskById
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime TaskStartDate { get; set; }
        public DateTime TaskEndDate { get; set; }
        public int TaskTypeId { get; set; }
        public int TaskStateId { get; set; }
        public int PriorityId { get; set; }
        public int ProjectId { get; set; }
        public string AssignedTo { get; set; }
        public List<string> PhotoPaths { get; set; } 
        public List<HttpPostedFileBase> Files { get; set; }
    }
}
