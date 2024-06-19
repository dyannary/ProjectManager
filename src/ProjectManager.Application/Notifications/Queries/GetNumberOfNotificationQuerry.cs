using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Notifications.Queries
{
    public class GetNumberOfNotificationQuerry : IRequest<int>
    {
        public int ForUserId { get; set; }
    }

    public class GetNumberOfNotificationCommand : IRequestHandler<GetNumberOfNotificationQuerry, int>
    {
        private readonly IAppDbContext _context;

        public GetNumberOfNotificationCommand(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetNumberOfNotificationQuerry request, CancellationToken cancellationToken)
        {
            var forUser = await _context.Users.FindAsync(request.ForUserId);

            var count = await _context.Notifications
                        .Where(n => n.UserId == forUser.Id)
                        .CountAsync();
            return count;
        }
    }
}
