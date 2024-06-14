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
    public class GetTaskStateQuery : IRequest<IEnumerable<TaskDropdownsDto>>
    {
    }

    public class GetTaskStateQueryHandlerGetTaskStateQuery : IRequestHandler<GetTaskStateQuery, IEnumerable<TaskDropdownsDto>>
    {
        private readonly IAppDbContext _context;

        public GetTaskStateQueryHandlerGetTaskStateQuery(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskDropdownsDto>> Handle(GetTaskStateQuery request, CancellationToken cancellationToken)
        {
            var taskState = await _context.ProjectTaskStates.ToListAsync(cancellationToken);

            var response = taskState.Select(t => new TaskDropdownsDto
            {
                Id = t.Id,
                Name = t.Name
            });

            return response;
        }
    }
}
