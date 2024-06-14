using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTasks.Queries
{
    public class GetTaskPriorityQuery : IRequest<IEnumerable<TaskDropdownsDto>>
    {
    }

    public class GetTaskPriorityQueryHandler : IRequestHandler<GetTaskPriorityQuery, IEnumerable<TaskDropdownsDto>>
    {
        private readonly IAppDbContext _context;

        public GetTaskPriorityQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDropdownsDto>> Handle(GetTaskPriorityQuery request, CancellationToken cancellationToken)
        {
            var taskPriorities = await _context.Priorities.ToListAsync(cancellationToken);

            var response = taskPriorities.Select(p => new TaskDropdownsDto
            {
                Id = p.Id,
                Name = p.Name
            });

            return response;
        }
    }
}
