using System.Collections.Generic;

namespace ProjectManager.Application.DataTransferObjects.NotificationDots
{
    public class NotificationResponseDto
    {
        public IEnumerable<NotificationListDto> NotificationListDtos {  get; set; }
    }
}
