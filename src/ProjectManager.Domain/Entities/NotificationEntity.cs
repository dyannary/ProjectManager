using ProjectManager.Domain.Common;
using System;

namespace ProjectManager.Domain.Entities
{
    public class NotificationEntity : Entity
    {
        public string Message { get; set; }
        public bool WasSeen { get; set; } = false;
        public DateTime Created { get; set; } = DateTime.Now;

        public int TypeId { get; set; }
        public NotificationType Type { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}