using ProjectManager.Application.DataTransferObjects.Projects;
using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.Projects
{
    public class ProjectResponseDto
    {
        public List<CardDto> Projects { get; set; }
        public int FromPage { get; set; }
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }
    }
}