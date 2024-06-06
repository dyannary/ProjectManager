using MediatR;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Create
{
    public class CreateProjectCollaboratorCommand : IRequest<bool>
    {
        public int ProjectId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user != null && user.Role.Name != "user")
            {
                return false;
            }

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);
            var ProjectUserRole = await _context.UserProjectRole.FirstOrDefaultAsync(pur => pur.Id == request.Role);

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
