using MediatR;
using ProjectManager.Application.interfaces;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Data.Entity;

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

            try
            {
                var userToDelete = await _context.Users.FirstOrDefaultAsync(p => p.Id.Equals(request.UserId));
                //var userToDelete = await _context.Users.FindAsync(userId, cancellationToken);
                if (userToDelete == null)
                {
                    return false;
                }

                if (userToDelete.IsEnabled)
                {
                    userToDelete.IsEnabled = false;
                }
                else
                    userToDelete.IsEnabled = true;

                await _context.SaveAsync(cancellationToken);

                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user with ID {request.UserId}: {ex.Message}");

                // Rethrow the exception if necessary
                throw;
            }

            
        }
    }
}
