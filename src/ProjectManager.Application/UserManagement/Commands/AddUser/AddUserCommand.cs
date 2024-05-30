using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Commands.CreateUser
{
    public class AddUserCommand : IRequest<bool>
    {
        public AddUserDto Data { get; set; }
    }

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public AddUserCommandHandler(
            IAppDbContext context,
            IPasswordEncryptionService passwordEncryptionService)
        {
            _context = context;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<bool> Handle(AddUserCommand command, CancellationToken cancellationToken)
        {
            var model = command.Data;

            if (model == null)
            {
                return false;
            }

            bool ifExists = _context.Users.Any(x => x.UserName == model.UserName);

            if(ifExists)
            {
                throw new Exception("User already exists");
            }

            var getRoles = _context.Roles.FirstOrDefaultAsync(x => x.Id == command.Data.RoleId);


            model.Password = _passwordEncryptionService.HashPassword(model.Password);

            var user = new Domain.Entities.User
            {
                UserName = model.UserName,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                IsEnabled = true,
                RoleId = command.Data.RoleId,
            };

            _context.Users.AddOrUpdate(user);

            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else 
                return false;
        }
    }
}
