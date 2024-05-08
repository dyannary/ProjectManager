using ProjectManager.Domain.Entities;
using System;

namespace ProjectManager.Domain.Common
{
    public abstract class AuditableEntity<T> : Entity<T>
    {
        public User CreatedBy { get; set; } = null;
        public DateTime Created { get; set; } = DateTime.Now;
        public User LastModifiedBy { get; set; } = null;
        public DateTime LastModified { get; set; } = DateTime.Now;
    }
}
