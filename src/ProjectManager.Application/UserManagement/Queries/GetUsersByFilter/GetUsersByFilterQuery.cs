using MediatR;
using ProjectManager.Application.Common.Extensions;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.TableParameters;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.Queries
{
    public class GetUsersByFilterQuery : IRequest<IEnumerable<UserDto>>
    {
        public DataTableParameters Parameters { get; set; }
        public GetUsersByFilterQuery(DataTableParameters parameters)
        {
            Parameters = parameters;
        }
    }
    public class GetUsersByFilterQueryHandler : IRequestHandler<GetUsersByFilterQuery, IEnumerable<UserDto>>
    {
        private readonly IAppDbContext _context;
        public GetUsersByFilterQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserDto>> Handle(GetUsersByFilterQuery request, CancellationToken cancellationToken)
        {
            var users = await _context.Users.Select(x=> new UserDto 
            { 
                Id = x.Id, 
                RoleId = x.RoleId,
                UserName = x.UserName,
                PhotoPath = x.PhotoPath,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                CreatedDate = x.Created,
                IsEnabled = x.IsEnabled,
                Actions = true,
            })
            .Search(request.Parameters).OrderBy(request.Parameters).Page(request.Parameters).ToListAsync();

            return users;
        }
    }
}
