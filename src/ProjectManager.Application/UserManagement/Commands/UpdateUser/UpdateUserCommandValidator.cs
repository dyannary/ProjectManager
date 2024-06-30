using ProjectManager.Application.interfaces;
using System.Text.RegularExpressions;
using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;
using System.Linq;
using System.Data.Entity;

namespace ProjectManager.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UserDto>
    {
        private readonly IAppDbContext _context;
        public UpdateUserCommandValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x)
                .MustAsync(async (x, _) =>
                {
                    var t = !(await _context.Users.AnyAsync(u => u.Id != x.Id && u.UserName == x.UserName));
                    return !await _context.Users.AnyAsync(u => u.Id != x.Id && u.UserName == x.UserName);
                })
                .WithMessage("Username already exists.")
                .WithName("UserName");

            RuleFor(model => model.UserName)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character.")
                .WithName("UserName");

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

            RuleFor(x => x)
                .MustAsync(async (x, _) =>
                {
                    var t = !(await _context.Users.AnyAsync(u => u.Id != x.Id && u.Email == x.Email));
                    return !await _context.Users.AnyAsync(u => u.Id != x.Id && u.Email == x.Email);
                })
                .WithMessage("Email already exists.")
                .WithName("Email");

            RuleFor(model => model.Email)
                .NotEmpty().WithMessage("This field is required.")
                .NotNull().WithMessage("This field is required.")
                .MaximumLength(50).WithMessage("Email can have maximum 50 character.")
                .Must(BeAValidEmail).WithMessage("Email must be a valid email address.")
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
    }
}
