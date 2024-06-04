using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.User.Queries
{
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
    {
        private readonly IAppDbContext _context;
        public GetUsersQueryHandler(IAppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.ToListAsync(cancellationToken);

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    CreatedDate = user.Created,
                    RoleId = user.RoleId,
                    IsEnabled = user.IsEnabled,
                };
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
