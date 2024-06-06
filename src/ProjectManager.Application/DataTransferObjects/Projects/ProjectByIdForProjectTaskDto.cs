using System.Web;
using System;

namespace ProjectManager.Application.DataTransferObjects.Projects
{
    public class ProjectByIdForProjectTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string PhotoPath { get; set; }
        public string ProjectState { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public string LoggedUserRole { get; set; }
        public string OwnerUserName { get; set; }
    }
}
