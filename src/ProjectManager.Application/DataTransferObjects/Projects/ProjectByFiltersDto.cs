using System;

namespace ProjectManager.Application.DataTransferObjects
{
    public class ProjectByFiltersDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ProjectEndDate { get; set; }
        public DateTime ProjectStartDate { get; set; }
        public string ProjectStateName { get; set; }
    }
}
