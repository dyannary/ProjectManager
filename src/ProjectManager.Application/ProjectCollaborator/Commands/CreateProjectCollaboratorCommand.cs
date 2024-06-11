using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCollaboratorCommand : IRequest<bool>
    {
        public CollaboratorToCreateDto collaboratorToCreateDto { get; set; }
    }

    public class CreateProjectCollaboratorHandler : IRequestHandler<CreateProjectCollaboratorCommand, bool>
    {
        private readonly IAppDbContext _context;

        public CreateProjectCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CreateProjectCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.collaboratorToCreateDto.UserName);
            if (user != null && user.Role.Name != "user")
            {
                return false;
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.collaboratorToCreateDto.ProjectId);
            var ProjectUserRole = await _context.UserProjectRole.FirstOrDefaultAsync(pur => pur.Id == request.collaboratorToCreateDto.RoleId);

            if (user != null && project != null && ProjectUserRole != null)
            {
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
                    return true;
                }
                catch
                {
                    return false;
                }

            }
            else return false;  
        }
    }
}
