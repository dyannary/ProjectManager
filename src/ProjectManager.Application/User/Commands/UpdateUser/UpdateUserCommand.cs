using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public UserDto Data { get; set; }
    }

    public class UpdateUserCommmandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly IAppDbContext _context;
        public UpdateUserCommmandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var updatedUser = command.Data;
            if (updatedUser == null)
                return null;

            var toUpdate = await _context.Users.FindAsync(updatedUser.Id, cancellationToken);
            if (toUpdate == null)
                return null;

            toUpdate.UserName = command.Data.UserName;
            toUpdate.FirstName = command.Data.FirstName;
            toUpdate.LastName = command.Data.LastName;
            toUpdate.Email = command.Data.Email;
            toUpdate.RoleId = command.Data.RoleId;
            toUpdate.IsEnabled = command.Data.IsEnabled;

            await _context.SaveAsync(cancellationToken);

            return updatedUser;
        }
    }
}
