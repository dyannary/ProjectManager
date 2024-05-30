using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public UserDto Data { get; set; }
    }

    public class UpdateUserCommmandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IAppDbContext _context;
        public UpdateUserCommmandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var updatedUser = command.Data;

            if (updatedUser == null)
                return false;

            var toUpdate = await _context.Users.FirstOrDefaultAsync(p => p.Id == command.Data.Id);

            if (toUpdate == null)
                return false;

            toUpdate.UserName = command.Data.UserName;
            toUpdate.FirstName = command.Data.FirstName;
            toUpdate.LastName = command.Data.LastName;
            toUpdate.Email = command.Data.Email;
            toUpdate.RoleId = command.Data.RoleId;
            toUpdate.IsEnabled = command.Data.IsEnabled;


            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else 
                return false;
        }
    }
}
