using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjectManager.Application.UserManagement.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserDto>
    {
        private readonly IAppDbContext _context;
        public AddUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(model => model.UserName)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character.")
                .Must(BeUniqueUserName).WithMessage("Username already exists.")
                .WithName("Name");

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character.")
                .Must(BeAValidPassword).WithMessage("Password must have at least 8 charactes, one non letter and digit.")
                .WithName("Password");

            RuleFor(model => model.FirstName)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("First Name can have maximum 50 character.")
                .WithName("FirstName");

            RuleFor(model => model.LastName)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Last Name can have maximum 50 character.")
                .WithName("LastName");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Email can have maximum 50 character.")
                .Must(BeAValidEmail).WithMessage("Email must be a valid email address.")
                .Must(BeUniqueEmail).WithMessage("Email already exists.")
                .WithName("Email");

            RuleFor(model => model.RoleId)
                .NotNull().WithMessage("This field is required.")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid role.")
                .WithName("RoleId");
        }

        private bool BeAValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return Regex.IsMatch(email, @"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");
        }

        private bool BeAValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$");
        }

        private bool BeUniqueUserName(string userName)
        {
            return !_context.Users.Any(x=>x.UserName == userName);
        }
        private bool BeUniqueEmail(string email)
        {
            return !_context.Users.Any(x => x.Email == email);
        }
    }
}

