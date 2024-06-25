using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.Queries.GetUserPhoto
{
    public class GetUserPhotoByEmailQuerry : IRequest<string>
    {
        public string Email { get; set; }
    }

    public class GetUserPhotoByEmailHandler : IRequestHandler<GetUserPhotoByEmailQuerry, string>
    {
        private readonly IAppDbContext _context;

        public GetUserPhotoByEmailHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetUserPhotoByEmailQuerry request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return $"/Content/Images/Default/default_avatar.jpg";
            }

            return user.PhotoPath;
        }
    }
}
