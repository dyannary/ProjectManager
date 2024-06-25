using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.Queries.GetUsername
{
    public class GetUsernameByEmailQuerry : IRequest<string>
    {
        public string Email { get; set; }
    }

    public class GetUsernameByEmailHandler : IRequestHandler<GetUsernameByEmailQuerry, string>
    {
        private readonly IAppDbContext _context;

        public GetUsernameByEmailHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetUsernameByEmailQuerry request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            return user != null ? user.UserName : "user";
        }
    }

}
