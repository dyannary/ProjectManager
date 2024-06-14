using MediatR;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain.Entities;
using System.Data.Entity.Migrations;

namespace ProjectManager.Application.ProjectTasks.Commands
{
    public class CreateTaskCommand : IRequest<bool>
    {
        public AddTaskDto Data { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
    {
        private readonly IAppDbContext _context;
        public CreateTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var model = command.Data;

            if (model == null)
            {
                return false;
            }

            var task = new Domain.Entities.ProjectTask
            { 
                Name = model.Name,
                Description = model.Description,
                TaskTypeId = model.TaskTypeId,
                TaskStateId = model.TaskStateId,
                PriorityId = model.PriorityId,
                ProjectId = model.ProjectId,
                TaskStartDate = System.DateTime.Now,
                TaskEndDate = System.DateTime.Now
            };

            var assignedUsers = await _context.Users.Where(user => model.AssignedTo.Contains(user.UserName)).ToListAsync();

            try
            {
                foreach (var user in assignedUsers)
                {
                    var userProjectTask = new UserProjectTask
                    {
                        UserId = user.Id,
                        ProjectTask = task
                    };

                    _context.UserProjectTasks.Add(userProjectTask);
                }
            }
            catch
            {
                throw;
            }

            _context.ProjectTasks.AddOrUpdate(task);

            int result = await _context.SaveAsync(cancellationToken);

            return result > 0;
        }

    }
}