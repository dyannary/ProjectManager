using System;

namespace ProjectManager.Application.DataTransferObjects.Projects
{
    public class ProjectByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string PhotoPath { get; set; }
        public int ProjectStateID {  get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectStartDate { get; set; }
    }
}
