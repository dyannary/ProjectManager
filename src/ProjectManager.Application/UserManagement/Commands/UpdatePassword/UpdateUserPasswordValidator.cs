using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;

namespace ProjectManager.Application.UserManagement.Commands.UpdatePassword
{
    public class UpdateUserPasswordValidator : AbstractValidator<UserPasswordChangeDto> {
        public UpdateUserPasswordValidator()
        {
            RuleFor(model => model.CurrentPassword).NotEmpty().WithMessage("Password cannot be empty")
                    .NotNull().WithMessage("Password cannot be null")
                    .MinimumLength(4).WithMessage("Password must be at least 4 characters");

            RuleFor(model => model.NewPassword).NotEmpty().WithMessage("Password cannot be empty")
                    .NotNull().WithMessage("Password cannot be null")
                    .MinimumLength(4).WithMessage("Password must be at least 4 characters");

            RuleFor(model => model.ConfirmPassword)
                    .Equal(model => model.NewPassword).WithName("New password and confirm password doesn't match")
                    .NotEmpty().WithMessage("Password cannot be empty")
                    .NotNull().WithMessage("Password cannot be null")
                    .MinimumLength(4).WithMessage("Password must be at least 4 characters");
        }
    }
}
