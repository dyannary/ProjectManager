using MediatR;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notifications.Commands.Send
{
    public class SendNotificationCommand : IRequest<bool>
    {
        public string ForUser_Username { get; set; }
        public int ProjectId { get; set; }
        public string NotificationType { get; set; }
        public string Message { get; set; }
    }

    public class SendNotidicationForCollaborator : IRequestHandler<SendNotificationCommand, bool>
    {
        public readonly IAppDbContext _context;

        public SendNotidicationForCollaborator(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SendNotificationCommand request, CancellationToken cancellationToken)
        {
            var forUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.ForUser_Username);
            var notificationType = await _context.NotificationTypes.FirstOrDefaultAsync(n => n.Name == request.NotificationType);
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
                return true;
            } catch
            {
                return false; 
            }


        }
    }

}
