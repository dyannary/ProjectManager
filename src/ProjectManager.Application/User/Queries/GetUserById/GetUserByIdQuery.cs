using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Queries
{
    public class GetUserByIdQuery : IRequest<UserByIdDto>
    {
        public int Id { get; set; } 
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserByIdDto>
    {
        private readonly IAppDbContext _context;
        public GetUserByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserByIdDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(request.Id, cancellationToken);

            //Pe viitor, message error
            if (user is null)
                return null;

            var userDto = new UserByIdDto
            {
                RoleId = user.RoleId,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };

            return userDto;
        }
    }
}