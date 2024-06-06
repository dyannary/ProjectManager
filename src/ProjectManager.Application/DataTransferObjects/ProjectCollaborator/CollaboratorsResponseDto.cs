using ProjectManager.Application.DataTransferObjects.ProjectTask.CollaboratorsManagement;
using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.ProjectCollaborator
{
    public class CollaboratorsResponseDto
    {
        public IEnumerable<CollaboratorsDetailsDto> CollaboratorsDetails { get; set; }
        public int FromPage { get; set; }
        public int MaxPage { get; set; }
        public int CurrentPage { get; set; }
        public int ProjectId { get; set; }
        public string ProjectCreatorName { get; set; }
        public string LoggedUserName { get; set; }
        public string LoggedUserRole { get; set; }
    }
}
