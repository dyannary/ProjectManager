using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCollaboratorCommand : IRequest<string>
    {
        public CollaboratorToCreateDto collaboratorToCreateDto { get; set; }
    }

    public class CreateProjectCollaboratorHandler : IRequestHandler<CreateProjectCollaboratorCommand, string>
    {
        private readonly IAppDbContext _context;

        public CreateProjectCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(CreateProjectCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.collaboratorToCreateDto.UserName);
            if (user != null && user.Role.Name != "user")
            {
                return "The server couldn't proccess the data!";
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.collaboratorToCreateDto.ProjectId);
            var ProjectUserRole = await _context.UserProjectRole.FirstOrDefaultAsync(pur => pur.Id == request.collaboratorToCreateDto.RoleId);

            if (user != null && project != null && ProjectUserRole != null)
            {
                if (user.UserProjects.Any(up => up.ProjectId == project.Id))
                    return "This user is already a collaborator for this project";

                var ProjectUser = new UserProject
                {
                    Project = project,
                    User = user,
                    ProjectRoleId = ProjectUserRole.Id,
                    UserProjectRole = ProjectUserRole,
                };

                try
                {
                    _context.UserProjects.Add(ProjectUser);
                    await _context.SaveAsync(cancellationToken);
                    return "success";
                }
                catch
                {
                    return "A problem occured on the server. Try again!";
                }

            }
            else return "The server couldn't procces the data";  
        }
    }
}
