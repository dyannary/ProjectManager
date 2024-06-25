using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectStates;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectStates.Queries
{
    public class GetProjectStatesQuerry : IRequest<IEnumerable<ProjectStatesSelectListDto>>
    {

    }

    public class GetProjectStatesHandler : IRequestHandler<GetProjectStatesQuerry, IEnumerable<ProjectStatesSelectListDto>>
    {
        private readonly IAppDbContext _context;
        public GetProjectStatesHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectStatesSelectListDto>> Handle(GetProjectStatesQuerry request, CancellationToken cancellationToken)
        {
            var projectStates = await _context.ProjectStates.ToListAsync(cancellationToken);

            var response = projectStates.Select(ps => new ProjectStatesSelectListDto
            {
                Id = ps.Id,
                Name = ps.Name,
            });

            return response;
        }
    }
}
