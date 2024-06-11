using FluentValidation;
using Microsoft.AspNetCore.Razor.Language;
using ProjectManager.Application.DataTransferObjects.Projects;
using System.IO;
using System.Web;

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCommandValidator : AbstractValidator<ProjectToCreateDto>
    {
        public CreateProjectCommandValidator() 
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Name cannot be empty")
                .NotNull().WithMessage("Name cannot be null")
                .MinimumLength(4).WithMessage("Name need to have at least 4 character")
                .MaximumLength(50).WithMessage("Name can have maximum 50 character")
                .WithName("Name");

            RuleFor(model => model.Description).NotEmpty().WithMessage("Description cannot be empty")
                .NotNull().WithMessage("Description cannot be null")
                .MinimumLength(4).WithMessage("Description need to have at least 50 character")
                .MaximumLength(250).WithMessage("Description can have maximum 250 character")
                .WithName("Description");

            RuleFor(model => model.File).Must(BeAPhoto).WithMessage("You can only upload images")
                .WithName("Image");

            RuleFor(model => model.ProjectStateID).NotNull().WithMessage("Project state cannot be null")
                .InclusiveBetween(1, 3).WithMessage("This is not a valid project state")
                .WithName("Project state");

            RuleFor(model => model.ProjectEndDate).NotNull().WithMessage("Project date cannot be null")
                .WithName("Project end date");
        }

        public bool BeAPhoto(HttpPostedFileBase file)
        {

            if (file == null)
                return true;

            string fileExtension = Path.GetExtension(file.FileName);

            try
            {
                if (fileExtension.Equals(".jpg") || fileExtension.Equals(".png") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".gif"))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

    }
}
