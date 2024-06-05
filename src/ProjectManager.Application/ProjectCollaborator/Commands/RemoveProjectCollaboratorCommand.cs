using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Commands
{
    public class RemoveProjectCollaboratorCommand : IRequest<bool>
    {
        public int ProjectId {  get; set; }
        public string CollaboratorUserName { get; set; }
    }

    public class RemoveProjectCollaboratorHandler : IRequestHandler<RemoveProjectCollaboratorCommand, bool>
    {

        private readonly IAppDbContext _context;

        public RemoveProjectCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(RemoveProjectCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.UserName == request.CollaboratorUserName);
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.ProjectId);

            if (project != null && user != null)
            {
                var userProject = _context.UserProjects.FirstOrDefault(up => up.UserId == user.Id && up.ProjectId == project.Id);

                try
                {
                    _context.UserProjects.Remove(userProject);
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
