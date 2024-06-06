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
                ProjectTaskType = task.ProjectTaskType.Name,
                ProjectTaskState = task.ProjectTaskState.Name,
                ProjectName = project.Name,
                Priority = task.Priority.Name,
                AssignedTo = task.UserProjectTasks.FirstOrDefault() != null ? $"{task.UserProjectTasks.FirstOrDefault().User.FirstName} {task.UserProjectTasks.FirstOrDefault().User.LastName}" : "Not Assigned",
                PhotoPath = task.UserProjectTasks.FirstOrDefault().User.PhotoPath,
            }).ToList();

            return tasks;
        }
    }
}
