namespace ProjectManager.Application.DataTransferObjects.Projects
{
    public class CardDto
    {
        public int Id { get; set; }    
        public string PhotoPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public bool IsEnabled { get; set; }
    }
}
