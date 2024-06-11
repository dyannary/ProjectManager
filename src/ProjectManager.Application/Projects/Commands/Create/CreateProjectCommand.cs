using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectManager.Domain.Entities;
using ProjectManager.Application.Interfaces;
using ProjectManager.Application.Enums;

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCommand : IRequest<bool>
    {
        public ProjectToCreateDto Project { get; set; }
        public int UserID { get; set; }
    }

    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, bool>
    {

        private readonly IAppDbContext _context;
        private readonly IFileService _fileService;
        public CreateProjectHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {

            FileTypeEnum fileType = FileTypeEnum.Default;
            string filePathResponse = _fileService.SaveFile(request.Project.File, fileType);


            if (request.Project.File != null)
            {
                fileType = FileTypeEnum.Image;
                filePathResponse = _fileService.SaveFile(request.Project.File, fileType);
            }
            else
            {
                filePathResponse = "/Content/Images/Default/defaultImage.jpg";
            }

            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == request.Project.ProjectStateID);
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserID);
            var userProjectRole = await _context.UserProjectRole.FirstOrDefaultAsync(upr => upr.Name == "ProjectCreator");
            var projectToCreate = new Project
            {
                Name = request.Project.Name,
                Description = request.Project.Description,
                PhotoPath = filePathResponse,
                ProjectStartDate = DateTime.Now,
                ProjectEndDate = request.Project.ProjectEndDate,
                Created = DateTime.Now,
                CreatedBy = request.UserID,
                LastModified = DateTime.Now,
                LastModifiedBy = request.UserID,
                ProjectState = projectState
            };

            var userProject = new UserProject
            {
                Project = projectToCreate,
                User = User,
                UserProjectRole = userProjectRole,
            };

            _context.Projects.Add(projectToCreate);
            _context.UserProjects.AddOrUpdate(userProject);
            try
            {
                await _context.SaveAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
