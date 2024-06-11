namespace ProjectManager.Application.DataTransferObjects.ProjectCollaborator
{
    public class CollaboratorToCreateDto
    {
        public int ProjectId { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
    }
}
