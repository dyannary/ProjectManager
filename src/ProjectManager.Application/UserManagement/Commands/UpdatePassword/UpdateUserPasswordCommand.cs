using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity.Migrations;

namespace ProjectManager.Application.UserManagement.Commands.UpdatePassword
{
    public class UpdateUserPasswordCommand : IRequest<string>
    {
        public UserPasswordChangeDto Passwords { get; set; }
    }
    public class UpdateUserPasswordHandler : IRequestHandler<UpdateUserPasswordCommand, string>
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public UpdateUserPasswordHandler(IAppDbContext context, IPasswordEncryptionService passwordEncryptionService)
        {
            _context = context;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<string> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            if (request.Passwords == null)
            {
                return "Datele nu pot fi nule";
            }

            var userToUpdate = await _context.Users.FindAsync(request.Passwords.Id);
            if (userToUpdate == null)
            {
                return "Nu a fost gasit user-ul";
            }

            if (!_passwordEncryptionService.VerifyPassword(request.Passwords.NewPassword, userToUpdate.Password))
            {
                var newPassword = _passwordEncryptionService.HashPassword(request.Passwords.NewPassword);

                userToUpdate.Password = newPassword;

                try
                {
                    _context.Users.AddOrUpdate(userToUpdate);
                    await _context.SaveAsync(cancellationToken);
                        return "succes";
                }
                catch
                {
                    return "error";
                }
            }
            else
            {
                return "Parola curenta nu este corecta";
            }

        }
    }

}
