using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;

namespace ProjectManager.Application.User.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserCommandValidator() 
        {
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Username cannot be empty")
                                .NotNull().WithMessage("Username cannot be null")
                                .MinimumLength(4).WithMessage("Username must be at least 4 characters");
            RuleFor(model => model.Password).NotEmpty().WithMessage("Password cannot be empty")
                                .NotNull().WithMessage("Password cannot be null")
                                .MinimumLength(4).WithMessage("Password must be at least 4 characters");
        }
    }
}
