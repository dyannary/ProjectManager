using System;

namespace ProjectManager.Application.DataTransferObjects.NotificationDots
{
    public class NotificationListDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
    }
}
