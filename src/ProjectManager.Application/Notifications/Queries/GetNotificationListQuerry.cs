using MediatR;
using ProjectManager.Application.DataTransferObjects.NotificationDots;
using ProjectManager.Application.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notification.Queries
{
    public class GetNotificationListQuerry : IRequest<IEnumerable<NotificationListDto>>
    {
        public int LoggedUserId { get; set; }
        public string type { get; set; }
    }

    public class GetNotificationListHandler : IRequestHandler<GetNotificationListQuerry, IEnumerable<NotificationListDto>>
    {
        public IAppDbContext _context { get; set; }

        public GetNotificationListHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NotificationListDto>> Handle(GetNotificationListQuerry request, CancellationToken cancellationToken)
        {
            var notifications = _context.Notifications.Where(n => n.UserId == request.LoggedUserId).AsQueryable();

            if (!string.IsNullOrEmpty(request.type) && request.type != "all")
            {
                notifications = notifications.Where(n => n.Type.Name == request.type);
            }

            var response = notifications.Select(n => new NotificationListDto
            {
                Id = n.Id,
                Created = n.Created,
                Message = n.Message,
                Type = n.Type.Name
            });

            return response;
        }
    }


}
