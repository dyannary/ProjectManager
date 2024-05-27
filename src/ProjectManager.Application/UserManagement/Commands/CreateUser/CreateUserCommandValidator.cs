using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;

namespace ProjectManager.Application.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<RegisterUserDto>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("Username is required");
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email address");
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required");
            RuleFor(x => x.IsEnabled)
                .NotEmpty()
                .WithMessage("Active status is required");
            RuleFor(x => x.RoleId)
                .NotEmpty()
                .WithMessage("Role is required");
        }
    }
}
