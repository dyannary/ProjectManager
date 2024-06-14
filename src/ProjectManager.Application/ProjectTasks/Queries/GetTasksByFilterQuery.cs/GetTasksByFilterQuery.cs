using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.TableParameters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using ProjectManager.Application.Common.Extensions;
using System.Data.Entity;

namespace ProjectManager.Application.ProjectTasks.Queries.GetTasksByFilterQuery.cs
{
    public class GetTasksByFilterQuery : IRequest<IEnumerable<TaskTableDto>>
    {
        public DataTableParameters Parameters { get; set; }
        public int ProjectId { get; set; }
        public GetTasksByFilterQuery(int projectId, DataTableParameters parameters)
        {
            ProjectId = projectId;
            Parameters = parameters;
        }
    }

    public class GetTasksByFilterQueryHandler : IRequestHandler<GetTasksByFilterQuery, IEnumerable<TaskTableDto>>
    {
        private readonly IAppDbContext _context;
        public GetTasksByFilterQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskTableDto>> Handle(GetTasksByFilterQuery request, CancellationToken cancellationToken)
        {

            var tasks = await _context.ProjectTasks
                .Include(u => u.UserProjectTasks)
                .Where(u => u.ProjectId == request.ProjectId)
                .Select(pt => new TaskTableDto
                {
                    Id = pt.Id,
                    Created = pt.Created,
                    Name = pt.Name,
                    AssignedTo = pt.UserProjectTasks.FirstOrDefault().User.UserName,
                   // AssignedTo = pt.UserProjectTasks.FirstOrDefault() != null ? $"{pt.UserProjectTasks.FirstOrDefault().User.FirstName} {pt.UserProjectTasks.FirstOrDefault().User.LastName}" : "Not Assigned",
                    ProjectTaskType = pt.ProjectTaskType.Name,
                    ProjectTaskState = pt.ProjectTaskState.Name,

                    Priority = pt.Priority.Name,
                    PhotoPath = pt.UserProjectTasks.FirstOrDefault().User.PhotoPath,
                })
                .Search(request.Parameters)
                .OrderBy(request.Parameters)
                .Page(request.Parameters)
                .ToListAsync();

            return tasks;
        }
    }
}
