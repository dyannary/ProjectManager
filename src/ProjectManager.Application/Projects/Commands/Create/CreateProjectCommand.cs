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

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCommand : IRequest<string>
    {
        public ProjectToCreateDto Project { get; set; }
        public int UserID { get; set; }
    }

    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, string>
    {

        private readonly IAppDbContext _context;
        private readonly IFileService _fileService;
        public CreateProjectHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<string> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            string filePathResponse = await _fileService.SaveFile(request.Project.File);

            if (filePathResponse == null)
            {
                return "Couldn't upload the file!";
            }

            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == request.Project.ProjectStateID);

            var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserID);

            var userProjectRole = await _context.UserProjectRole.FirstOrDefaultAsync(upr => upr.Name == "ProjectCreator");

            if (projectState == null || User == null || userProjectRole == null)
            {
                return "The server coundn't process the data!";
            }


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

            try
            {
                _context.Projects.Add(projectToCreate);
                _context.UserProjects.AddOrUpdate(userProject);
                await _context.SaveAsync(cancellationToken);
                return "success";
            }
            catch
            {
                return "A problem on the server occured";
            }
        }
    }

}
