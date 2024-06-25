using FluentValidation;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;

namespace ProjectManager.Application.ProjectCollaborator.Commands
{
    public class CreateProjectCollaboratorCommandValidator : AbstractValidator<CollaboratorToCreateDto>
    {
        public CreateProjectCollaboratorCommandValidator() 
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username cannot be empty")
                .NotNull().WithMessage("Username cannot be null")
                .MinimumLength(4).WithMessage("Username need to have at least 4 character")
                .MaximumLength(50).WithMessage("Username can have maximum 50 character")
                .WithName("Username");


            RuleFor(model => model.RoleId).NotNull().WithMessage("Role cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid Role")
                .WithName("Role");
        }
    }
}
