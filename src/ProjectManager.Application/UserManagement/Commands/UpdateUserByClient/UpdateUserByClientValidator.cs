using FluentValidation;
using ProjectManager.Application.DataTransferObjects.User;

namespace ProjectManager.Application.UserManagement.Commands.UpdateUserByClient
{
    public class UpdateUserByClientValidator : AbstractValidator<UserByIdForClientDto>
    {
        public UpdateUserByClientValidator() 
        {
            RuleFor(model => model.Username).NotNull().WithMessage("Username can't be null")
                                 .NotEmpty().WithMessage("Username can't be empty")
                                 .MinimumLength(4).WithMessage("Username needs to be at least 4 characters")
                                 .MaximumLength(20).WithMessage("Username can be maximul 20 characters");

            RuleFor(model => model.FirstName).NotNull().WithMessage("FirstName can't be null")
                                .NotEmpty().WithMessage("FirstName can't be empty")
                                .MinimumLength(4).WithMessage("FirstName needs to be at least 4 characters")
                                .MaximumLength(50).WithMessage("FirstName can be maximul 50 characters");


            RuleFor(model => model.LastName).NotNull().WithMessage("LastName can't be null")
                                .NotEmpty().WithMessage("LastName can't be empty")
                                .MinimumLength(4).WithMessage("LastName needs to be at least 4 characters")
                                .MaximumLength(50).WithMessage("LastName can be maximul 50 characters");
        }
    }
}
