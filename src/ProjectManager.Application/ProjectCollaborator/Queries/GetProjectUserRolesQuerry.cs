using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Queries
{
    public class GetProjectUserRolesQuerry : IRequest<IEnumerable<CollaboratorsProjectRolesDto>>
    {
    }

    public class GetProjectUserRolesHandler : IRequestHandler<GetProjectUserRolesQuerry, IEnumerable<CollaboratorsProjectRolesDto>>
    {
        private readonly IAppDbContext _context;

        public GetProjectUserRolesHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CollaboratorsProjectRolesDto>> Handle(GetProjectUserRolesQuerry request, CancellationToken cancellationToken)
        {
            var projectRoles = await _context.UserProjectRole
                .Where(u => u.Name != "ProjectCreator")
                .ToListAsync();

            var response = projectRoles.Select(pr => new CollaboratorsProjectRolesDto
            {
                Id = pr.Id,
                Name = pr.Name
            }).ToList();

            return response;
        }
    }

}
