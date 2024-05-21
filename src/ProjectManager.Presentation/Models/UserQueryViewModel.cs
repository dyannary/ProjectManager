namespace ProjectManager.Presentation.Models
{
    public class UserQueryViewModel
    {
        public string SearchTerm { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}