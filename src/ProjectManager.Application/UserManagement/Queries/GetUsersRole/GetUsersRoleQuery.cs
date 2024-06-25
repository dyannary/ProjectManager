using MediatR;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using ProjectManager.Application.DataTransferObjects.Projects;
using System.Data.Entity;
using System.Linq;

namespace ProjectManager.Application.UserManagement.Queries.GetUsersRole
{
    public class GetUsersRoleQuery : IRequest<IEnumerable<RoleDto>>
    {

    }

    public class GetUsersRoleQueryHandler : IRequestHandler<GetUsersRoleQuery, IEnumerable<RoleDto>>
    {
        private readonly IAppDbContext _context;
        public GetUsersRoleQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetUsersRoleQuery request, CancellationToken cancellationToken)
        {
            var usersRole = await _context.Roles.ToListAsync(cancellationToken);

            var response = usersRole.Select(ps => new RoleDto
            {
                Id = ps.Id,
                Name = ps.Name,
            });

            return response;
        }
    }
}
