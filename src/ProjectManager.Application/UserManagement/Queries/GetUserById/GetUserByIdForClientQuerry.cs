using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.Queries
{
    public class GetUserByIdForClientQuerry : IRequest<UserByIdForClientDto>
    {
        public int UserId { get; set; }
    }

    public class GetUserByIdForClientHandler : IRequestHandler<GetUserByIdForClientQuerry, UserByIdForClientDto>
    {
        private readonly IAppDbContext _context;

        public GetUserByIdForClientHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<UserByIdForClientDto> Handle(GetUserByIdForClientQuerry request, CancellationToken cancellationToken)
        {
            var userById = await _context.Users.FindAsync(request.UserId);

            if (userById == null)
            {
                return null;
            }

            return new UserByIdForClientDto
            {
                Id = userById.Id,
                FirstName = userById.FirstName,
                LastName = userById.LastName,
                Username = userById.UserName,
            };
        }
    }

}
