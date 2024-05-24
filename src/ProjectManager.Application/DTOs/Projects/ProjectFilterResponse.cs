using System.Collections.Generic;

namespace ProjectManager.Application.DTOs.Projects
{
    public class ProjectFilterResponse
    {
        public List<CardDto> Cards { get; set; }
        public int MaxPage { get; set; }
        public int FromPage { get; set; }
    }
}
