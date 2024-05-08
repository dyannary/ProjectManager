using ProjectManager.Domain.Common;

namespace ProjectManager.Domain.Entities
{
    public class File : AuditableEntity<int>
    {
        public int FileName { get; set; }
        public string FileData { get; set; }
        public bool IsDeleted { get; set; }

        public int FileTypeId { get; set; }
        public virtual FileType FileType { get; set; }

        public int ProjectTaskId { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }

    }
}
