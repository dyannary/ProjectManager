using MediatR;
using ProjectManager.Application.Enums;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Services;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notifications.Commands.Send
{
    public class SendNotificationCommand : IRequest<bool>
    {
        public string ForUser_Username { get; set; }
        public int ForUser_Id { get; set; } = -1;
        public int ProjectId { get; set; }
        public NotificationTypeEnum NotificationType { get; set; }
        public string Message { get; set; }
    }

    public class SendNotificationCommandHandler : IRequestHandler<SendNotificationCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly INotificationService _notificationService;

        public SendNotificationCommandHandler (IAppDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            string userUsername = request.ForUser_Username;
            if (request.ForUser_Id != -1)
            {
                var userTemp = await _context.Users.FindAsync(request.ForUser_Id);
                userUsername = userTemp.UserName;
            }

            var forUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userUsername);
            var notificationType = await _context.NotificationTypes.FirstOrDefaultAsync(n => n.Name == request.NotificationType.ToString().Replace("_", " "));
            var project = await _context.Projects.FindAsync(request.ProjectId);

            if (forUser == null || notificationType == null)
                return false;

            var notificationToSend = new NotificationEntity
            {
                Message = request.Message + project.Name,
                User = forUser,
                Type = notificationType,
                WasSeen = false,
            };

            try
            {
                _context.Notifications.Add(notificationToSend);
                await _context.SaveAsync(cancellationToken);

                int count = await _context.Notifications.CountAsync();
                await _notificationService.NotifyAsync(count, forUser.Email);

                return true;
            } catch
            {
                return false; 
            }


        }
    }

}
