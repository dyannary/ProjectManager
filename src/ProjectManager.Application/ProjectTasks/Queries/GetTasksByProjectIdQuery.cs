using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace ProjectManager.Application.ProjectTasks.Queries
{
    public class GetTasksByProjectId : IRequest<IEnumerable<ProjectTaskById>>
    {
        public int Id { get; set; }
    }
    public class GetTasksByProjectIdHandler : IRequestHandler<GetTasksByProjectId, IEnumerable<ProjectTaskById>>
    {
        private readonly IAppDbContext _context;
        public GetTasksByProjectIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTaskById>> Handle(GetTasksByProjectId request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.Include(x=>x.ProjectTasks).FirstOrDefaultAsync(p => p.Id == request.Id);

            if (project == null)
            {
                return null;
            }

            var tasks = project.ProjectTasks.Select(task => new ProjectTaskById
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                TaskStartDate = task.TaskStartDate,
                TaskEndDate = task.TaskEndDate,
                PriorityId = task.PriorityId,
                TaskStateId = task.ProjectTaskState.Id,
                TaskTypeId = task.ProjectTaskType.Id,
                AssignedTo = task.UserProjectTasks.FirstOrDefault().User.UserName,
                PhotoPath = task.UserProjectTasks.FirstOrDefault()?.User?.PhotoPath,
            }).ToList();

            return tasks;
        }
    }
}
