using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using ProjectManager.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
        private readonly IFileService _fileService;

        public UpdateTaskCommandHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }
        public async Task<bool> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var toUpdate = await _context.ProjectTasks.Include(pt => pt.UserProjectTasks).FirstOrDefaultAsync(p => p.Id == command.Data.Id);

            var taskFiles = command.Data.Files;

            if (toUpdate == null)
                return false;

            toUpdate.Name = command.Data.Name;
            toUpdate.Description = command.Data.Description;
            toUpdate.TaskTypeId = command.Data.TaskTypeId;
            toUpdate.TaskStateId = command.Data.TaskStateId;
            toUpdate.PriorityId = command.Data.PriorityId;

            if (taskFiles != null)
            {
                foreach (var file in taskFiles)
                {
                    var filePath = await _fileService.SaveFile(file);

                    var taskFile = new File
                    {
                        FileName = filePath,
                        ProjectTaskId = toUpdate.Id,
                        FileData = "img"
                    };

                    try
                    {
                        _context.Files.Add(taskFile);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        throw;
                    }
                }
            }

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

            try
            {
                int result = await _context.SaveAsync(cancellationToken);
                return result > 0;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entity of type {eve.Entry.Entity.GetType().Name} in state {eve.Entry.State} has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine($"- Property: {ve.PropertyName}, Error: {ve.ErrorMessage}");
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
