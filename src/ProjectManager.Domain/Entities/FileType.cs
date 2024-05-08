using ProjectManager.Domain.Common;
using System.Collections.Generic;

namespace ProjectManager.Domain.Entities
{
    public class FileType : Entity<int>
    {
        public string Type { get; set; }

        public virtual ICollection<File> File { get; set; }
    }
}
