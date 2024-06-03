using Autofac;
using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectsForDropDowmQuerry : IRequest<IEnumerable<ProjectForDropDownDto>>
    {

    }

    public class GetProjectsForDropDownHandler : IRequestHandler<GetProjectsForDropDowmQuerry, IEnumerable<ProjectForDropDownDto>>
    {
        public readonly IAppDbContext _context;
        public GetProjectsForDropDownHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectForDropDownDto>> Handle(GetProjectsForDropDowmQuerry request, CancellationToken cancellationToken)
        {
            var querry = await _context.Projects.ToListAsync();

            var response = querry.Select(p => new ProjectForDropDownDto
            {
                Id = p.Id,
                Name = p.Name,
            });

            return response;
        }
    }
}
