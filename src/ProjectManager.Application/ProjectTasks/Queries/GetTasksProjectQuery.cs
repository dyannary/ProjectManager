using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTasks.Queries
{
    public class GetTasksProjectQuery : IRequest<IEnumerable<ProjectByIdForProjectTaskDto>>
    {

    }

    public class GetTasksProjectQueryHandler : IRequestHandler<GetTasksProjectQuery, IEnumerable<ProjectByIdForProjectTaskDto>>
    {
        private readonly IAppDbContext _context;
        public GetTasksProjectQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectByIdForProjectTaskDto>> Handle(GetTasksProjectQuery request, CancellationToken cancellationToken)
        {
            var tasksProject = await _context.Projects.ToListAsync(cancellationToken);

            var response = tasksProject.Select(ps => new ProjectByIdForProjectTaskDto
            {
                Id = ps.Id,
                Name = ps.Name,
            });

            return response;
        }
    }
}
