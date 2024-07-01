using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Linq;

namespace ProjectManager.Application.ProjectTasks.Queries
{
    public class GetProjectByTaskIdQuery : IRequest<IEnumerable<TaskDropdownsDto>>
    {
        public int Id { get; set; }
    }

    public class GetProjectByTaskIdQueryHandler : IRequestHandler<GetProjectByTaskIdQuery, IEnumerable<TaskDropdownsDto>>
    {
        private readonly IAppDbContext _context;
        public GetProjectByTaskIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDropdownsDto>> Handle(GetProjectByTaskIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Include(pt => pt.ProjectTasks).FirstOrDefaultAsync(p => p.ProjectTasks.Any(pt => pt.Id == request.Id));

            var users = project.UserProjects.Select(user => new TaskDropdownsDto
            {
                Id = user.UserId,
                Name = user.User.UserName
            });

            return users;
        }
    }
}
