using FluentValidation;
using ProjectManager.Application.DataTransferObjects.ProjectTask;

namespace ProjectManager.Application.ProjectTasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<AddTaskDto>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Name cannot be empty")
                .NotNull().WithMessage("Name cannot be null")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character")
                .WithName("Name");

            RuleFor(model => model.TaskStateId).NotNull().WithMessage("Task state cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid task state")
                .WithName("Task state");

            RuleFor(model => model.TaskTypeId).NotNull().WithMessage("Task type cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid task type")
                .WithName("Task type");

            RuleFor(model => model.PriorityId).NotNull().WithMessage("Priority type type cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid priority type")
                .WithName("Priority");
        }
    }
}
