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
    public class GetTaskTypeQuery : IRequest<IEnumerable<TaskDropdownsDto>>
    {
    }

    public class GetTaskTypeQueryHandler : IRequestHandler<GetTaskTypeQuery, IEnumerable<TaskDropdownsDto>>
    {
        private readonly IAppDbContext _context;

        public GetTaskTypeQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskDropdownsDto>> Handle(GetTaskTypeQuery request, CancellationToken cancellationToken)
        {
            var taskTypes = await _context.ProjectTaskTypes.ToListAsync(cancellationToken);

            var response = taskTypes.Select(t => new TaskDropdownsDto
            {
                Id = t.Id,
                Name = t.Name,
            });

            return response;
        }
    }
}
