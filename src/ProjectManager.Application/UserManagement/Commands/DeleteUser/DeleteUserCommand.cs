using MediatR;
using ProjectManager.Application.interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IAppDbContext _context;
        public DeleteUserCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if(request == null)
            {
                return false;
            }

            int userId = request.UserId;

            var userToDelete = await _context.Users.FindAsync(userId, cancellationToken);

            if(userToDelete == null)
            {
                return false;
            }

            userToDelete.IsEnabled = false;

            await _context.SaveAsync(cancellationToken);

            return true;
        }
    }
}
