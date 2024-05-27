using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Queries
{
    public class GetUserByUsernameAndPasswordQuerry : IRequest<UserByUsernameAndPasswordDto>
    {
        public string UserName { get; set; }    
        public string Password { get; set; }
    }

    public class GetUserByUsernameAndPasswordHandler : IRequestHandler<GetUserByUsernameAndPasswordQuerry, UserByUsernameAndPasswordDto>
    {

        private readonly IAppDbContext _context;
        private readonly IPasswordEncryptionService _passwordEncryptionService;

        public GetUserByUsernameAndPasswordHandler(IAppDbContext context, IPasswordEncryptionService passwordEncryptionService)
        {
            _context = context;
            _passwordEncryptionService = passwordEncryptionService;
        }


        public async Task<UserByUsernameAndPasswordDto> Handle(GetUserByUsernameAndPasswordQuerry request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName.ToUpper() == request.UserName.ToUpper(), cancellationToken);

                if (user == null)
                {
                    return null;
                }

                if (!_passwordEncryptionService.VerifyPassword(request.Password, user.Password))
                    return null;

                var userByUsernameAndPasswordDto = new UserByUsernameAndPasswordDto
                {
                    id = user.Id,
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Role = user.Role.Name,
                    IsEnabled = user.IsEnabled
                };
                return userByUsernameAndPasswordDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured in user query" + ex);
            }
        }
    }

}
