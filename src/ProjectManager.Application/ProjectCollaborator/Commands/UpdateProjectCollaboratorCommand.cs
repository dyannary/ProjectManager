using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Update
{
    public class UpdateProjectCollaboratorCommand : IRequest<string>
    {
        public int ProjectId { get; set; }
        public string UserName { get; set; }
        public int Role { get; set; }
    }

    public class UpdateProjectCollaboratorHandler : IRequestHandler<UpdateProjectCollaboratorCommand, string>
    {
        private readonly IAppDbContext _context;

        public UpdateProjectCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(UpdateProjectCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = await _context.Users.FirstOrDefaultAsync(p => p.UserName == request.UserName);
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);
            var ProjectUserRole = await _context.UserProjectRole.FirstOrDefaultAsync(pur => pur.Id == request.Role);

            if (project != null && ProjectUserRole != null)
            {
                var userProject = _context.UserProjects.FirstOrDefault(up => up.UserId == userToAdd.Id && up.ProjectId == project.Id);

                userProject.UserProjectRole = ProjectUserRole;

                try
                {
                    _context.UserProjects.AddOrUpdate(userProject);
                    await _context.SaveAsync(cancellationToken);
                    return "success";
                }
                catch
                {
                    return "A problem occured on the server. Try again";
                }

            }
            else return "The server couldn't procces the data. Try again";
        }
    }
}
