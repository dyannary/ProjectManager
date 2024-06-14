using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTasks.Queries
{
    public class GetTaskByIdQuery : IRequest<ProjectTaskById>
    {
        public int Id { get; set; } 
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ProjectTaskById>
    {
        private readonly IAppDbContext _context;

        public GetTaskByIdQueryHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<ProjectTaskById> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.ProjectTasks
                .Include(up=>up.UserProjectTasks)
                .FirstOrDefaultAsync(p=>p.Id == request.Id);

            if(task == null)
                return null;

            var taskDto = new ProjectTaskById
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                CreatedDate = task.Created,
                TaskStartDate = task.TaskStartDate,
                TaskEndDate = task.TaskEndDate,
                TaskTypeId = task.ProjectTaskType.Id,
                TaskStateId = task.ProjectTaskState.Id,
                PriorityId = task.Priority.Id,
                ProjectId = task.ProjectId
            };

            if (task.UserProjectTasks.Any())
            {
                taskDto.AssignedTo = task.UserProjectTasks.First().User.UserName;
            }
            else
            {
                taskDto.AssignedTo = "Unassigned";
            }

            return taskDto;
        }
    }
}
