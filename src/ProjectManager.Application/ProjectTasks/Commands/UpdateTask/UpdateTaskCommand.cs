using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTasks.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public UpdateTaskDto Data { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IAppDbContext _context;

        public UpdateTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _context.ProjectTasks.Include(pt => pt.UserProjectTasks).FirstOrDefaultAsync(p => p.Id == command.Data.Id);

            if (toUpdate == null)
                return false;

            toUpdate.Name = command.Data.Name;
            toUpdate.Description = command.Data.Description;
            toUpdate.TaskTypeId = command.Data.TaskTypeId;
            toUpdate.TaskStateId = command.Data.TaskStateId;
            toUpdate.PriorityId = command.Data.PriorityId;

            var assignedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == command.Data.AssignedTo);
            if (assignedUser != null)
            {
                toUpdate.UserProjectTasks.Clear();

                toUpdate.UserProjectTasks.Add(new UserProjectTask
                {
                    UserId = assignedUser.Id,
                    ProjectTask = toUpdate
                });
            }

            int result = await _context.SaveAsync(cancellationToken);

            return result > 0;
        }
    }
}
