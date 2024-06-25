using System;
using System.Web;

namespace ProjectManager.Application.DataTransferObjects.Projects
{
    public class ProjectToCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase File { get; set; }
        public int ProjectStateID { get; set; }
        public DateTime ProjectEndDate { get; set; }
    }
}
