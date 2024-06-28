using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;
using System.Text.RegularExpressions;

namespace ProjectManager.Application.UserManagement.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserDto>
    {
        public AddUserCommandValidator()
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username cannot be empty")
                .NotNull().WithMessage("Username cannot be null")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character")
                .WithName("Name");

            RuleFor(model => model.Password).NotEmpty().WithMessage("Password cannot be empty")
                .NotNull().WithMessage("Password cannot be null")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character")
                .WithName("Password");

            RuleFor(model => model.FirstName).NotEmpty().WithMessage("First Name cannot be empty")
                .NotNull().WithMessage("First Name cannot be null")
                .MaximumLength(50).WithMessage("First Name can have maximum 50 character")
                .WithName("FirstName");

            RuleFor(model => model.LastName).NotEmpty().WithMessage("Last Name cannot be empty")
                .NotNull().WithMessage("Last Name cannot be null")
                .MaximumLength(50).WithMessage("Last Name can have maximum 50 character")
                .WithName("LastName");

            RuleFor(model => model.Email).NotEmpty().WithMessage("Email cannot be empty")
                .NotNull().WithMessage("Email cannot be null")
                .MaximumLength(50).WithMessage("Email can have maximum 50 character")
                .Must(BeAValidEmail).WithMessage("Email must be a valid email address")
                .WithName("Email");

            RuleFor(model => model.RoleId).NotNull().WithMessage("Role cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid role")
                .WithName("RoleId");
        }

        private bool BeAValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return Regex.IsMatch(email, @"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");
        }
    }
}

