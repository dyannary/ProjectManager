using MediatR;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Domain.Entities;
using System.Data.Entity.Migrations;
using ProjectManager.Application.Interfaces;
using System;
using System.Data.Entity.Validation;

namespace ProjectManager.Application.ProjectTasks.Commands
{
    public class CreateTaskCommand : IRequest<bool>
    {
        public AddTaskDto Data { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, bool>
    {
        private readonly IAppDbContext _context;
        private readonly IFileService _fileService;

        public CreateTaskCommandHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<bool> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
        {
            var model = command.Data;

            if (model == null)
            {
                return false;
            }

            var task = new ProjectTask
            {
                Name = model.Name,
                Description = model.Description,
                TaskTypeId = model.TaskTypeId,
                TaskStateId = model.TaskStateId,
                PriorityId = model.PriorityId,
                ProjectId = model.ProjectId,
                TaskStartDate = DateTime.Now,
                TaskEndDate = DateTime.Now
            };

            if (model.Files != null) 
            {
                foreach (var file in model.Files)
                {
                    var filePath = await _fileService.SaveFile(file);

                    var taskFile = new File
                    {
                        FileName = filePath,
                        ProjectTask = task,
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

            var assignedUsers = await _context.Users.Where(user => user.Id == model.AssignedTo).ToListAsync();

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