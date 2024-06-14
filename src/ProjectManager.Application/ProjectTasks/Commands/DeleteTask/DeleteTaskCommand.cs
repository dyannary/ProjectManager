using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectTasks.Commands.DeleteTask
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public int TaskId { get; set; }
    }
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IAppDbContext _context;
        public DeleteTaskCommandHandler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return false;
            }

            try
            {
                var taskToDelete = await _context.ProjectTasks.FirstOrDefaultAsync(p => p.Id.Equals(request.TaskId));

                if (taskToDelete == null)
                    return false;

                //Delete task

                _context.ProjectTasks.Remove(taskToDelete);

                await _context.SaveAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user with ID {request.TaskId}: {ex.Message}");

                return false;
            }
        }
    }
}
