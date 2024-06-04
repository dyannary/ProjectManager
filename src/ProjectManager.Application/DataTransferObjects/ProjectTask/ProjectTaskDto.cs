using ProjectManager.Application.DataTransferObjects.Projects;
using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.ProjectTask
{
    public class ProjectTaskDto
    {
        public ProjectByIdForProjectTaskDto Project { get; set; }  
        public List<ProjectForDropDownDto> ProjectsList { get; set; }
        public List<ProjectTaskById> ProjectTasks { get; set; }
        // The list of tasks
    }
}
