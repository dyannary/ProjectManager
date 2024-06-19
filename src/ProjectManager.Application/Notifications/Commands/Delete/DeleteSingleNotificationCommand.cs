using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notifications.Commands
{
    public class DeleteSingleNotificationCommand : IRequest<bool>
    {
        public int NotificationId { get; set; }
        public int LoggedUserId { get; set; }
    }

    public class DeleteSingleNitificationHandler : IRequestHandler<DeleteSingleNotificationCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteSingleNitificationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteSingleNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationToDelete = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == request.NotificationId && n.UserId == request.LoggedUserId);

            if (notificationToDelete == null)
            {
                return false;
            }

            try
            {
                _context.Notifications.Remove(notificationToDelete);
                await _context.SaveAsync(cancellationToken);
                return true;
            } catch
            {
                return false;
            }
        }
    }

}
