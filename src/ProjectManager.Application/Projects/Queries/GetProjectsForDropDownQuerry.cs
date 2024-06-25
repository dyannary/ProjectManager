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
    public class GetProjectsForDropDownQuerry : IRequest<IEnumerable<ProjectForDropDownDto>>
    {
        public int UserID { get; set; }
    }

    public class GetProjectsForDropDownHandler : IRequestHandler<GetProjectsForDropDownQuerry, IEnumerable<ProjectForDropDownDto>>
    {
        public readonly IAppDbContext _context;
        public GetProjectsForDropDownHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectForDropDownDto>> Handle(GetProjectsForDropDownQuerry request, CancellationToken cancellationToken)
        {
            var querry = await _context.Projects
                .Where(p => p.UserProjects.Any(up => up.UserId == request.UserID))
                .ToListAsync();

            var response = querry.Select(p => new ProjectForDropDownDto
            {
                Id = p.Id,
                Name = p.Name,
            });

            return response;
        }
    }
}
