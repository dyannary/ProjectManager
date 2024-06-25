using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notifications.Commands
{
    public class DeleteMultipleNotificationCommand : IRequest<bool>
    {
        public int LoggedUserId { get; set; }
    }

    public class DeleteMultipleNotificationHandler : IRequestHandler<DeleteMultipleNotificationCommand, bool>
    {
        private readonly IAppDbContext _context;

        public DeleteMultipleNotificationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMultipleNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationsToDelete = await _context.Notifications
                                .Where(n => n.UserId == request.LoggedUserId)
                                .ToListAsync();
            try
            {
                _context.Notifications.RemoveRange(notificationsToDelete);
                await _context.SaveAsync(cancellationToken);
                return true;
            } catch
            {
                return false;
            }

        }
    }

}
