using ProjectManager.Domain.Common;

namespace ProjectManager.Domain.Entities
{
    public class File : Entity
    {
        public string FileName { get; set; }
        public string FileData { get; set; }
        public bool IsDeleted { get; set; }
        public int ProjectTaskId { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
    }
}
