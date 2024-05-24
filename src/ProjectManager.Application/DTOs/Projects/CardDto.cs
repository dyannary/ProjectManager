namespace ProjectManager.Application.DTOs.Projects
{
    public class CardDto
    {
        public int Id { get; set; }    
        public string PhotoPath { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string status { get; set; }
        public bool IsEnabled { get; set; }
    }
}
