﻿using FluentValidation;
using ProjectManager.Application.DataTransferObjects.ProjectTask;

namespace ProjectManager.Application.ProjectTasks.Commands.CreateTask
{
    public class CreateTaskCommandValidator : AbstractValidator<AddTaskDto>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character")
                .WithName("Name");

            RuleFor(model => model.TaskStateId).NotNull().WithMessage("This field is required.")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid state.")
                .WithName("Task state");

            RuleFor(model => model.TaskTypeId).NotNull().WithMessage("This field is required.")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid type.")
                .WithName("Task type");

            RuleFor(model => model.PriorityId).NotNull().WithMessage("This field is required.")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid priority.")
                .WithName("Priority");
        }
    }
}
