using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectManager.Domain.Entities;

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
        public CreateProjectHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {

            // TODO: Add functionality for user to choose the project state
            // TODO: Add functionality for user to add its own photo for project
            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == 1);
            var User = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserID);
            var userProjectRole = await _context.UserProjectRole.FirstOrDefaultAsync(upr => upr.Name == "ProjectCreator");
            var projectToCreate = new Project
            {
                Name = request.Project.Name,
                Description = request.Project.Description,
                PhotoPath = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Default_pfp.svg/2048px-Default_pfp.svg.png",
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

            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else return false;
        }
    }

}
