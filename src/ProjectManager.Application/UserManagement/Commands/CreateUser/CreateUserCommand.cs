using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<RegisterUserDto>
    {
        public RegisterUserDto Data { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, RegisterUserDto>
    {
        private readonly IAppDbContext _context;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public CreateUserCommandHandler(
            IAppDbContext context,
            IPasswordEncryptionService passwordEncryptionService)
        {
            _context = context;
            _passwordEncryptionService = passwordEncryptionService;
        }

        public async Task<RegisterUserDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var model = command.Data;

            if (model == null)
            {
                return null;
            }

            bool ifExists = _context.Users.Any(x => x.UserName == model.UserName);

            if(ifExists)
            {
                throw new Exception("User already exists");
            }
            model.Password = _passwordEncryptionService.HashPassword(model.Password);

            var user = new Domain.Entities.User
            {
                UserName = model.UserName,
            };

            _context.Users.AddOrUpdate(user);

            await _context.SaveAsync(cancellationToken);

            return model;
        }
    }
}
