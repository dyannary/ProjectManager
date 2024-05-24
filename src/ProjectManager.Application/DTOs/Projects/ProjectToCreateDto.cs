using System;

namespace ProjectManager.Application.DTOs.Projects
{
    public class ProjectToCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        public int ProjectStateID { get; set; }
        public DateTime ProjectEndDate { get; set; }
    }
}
