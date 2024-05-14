using ProjectManager.Domain.Entities;
using System;

namespace ProjectManager.Domain.Common
{
    public abstract class AuditableEntity : Entity
    {
        public int CreatedBy { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; } = DateTime.Now;
    }
}
