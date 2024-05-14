using MediatR;
using ProjectManager.Application.DTOs.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<LoginUserDto>
    {
        public LoginUserDto LoginUserDto { get; set; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserDto>
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordEncryptionService _passwordEncryptionService; 
        public LoginUserCommandHandler(
            IAppDbContext context, 
            IPasswordEncryptionService passwordEncryptionService)
        {
            _context = context; 
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<LoginUserDto> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == command.LoginUserDto.UserName);

            //VerifyPassword

            //Add more claims
            //Roles
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            return command.LoginUserDto;
        }
    }
}
