using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Commands
{
    public class RemoveProjectCollaboratorCommand : IRequest<string>
    {
        public int ProjectId {  get; set; }
        public string CollaboratorUserName { get; set; }
    }

    public class RemoveProjectCollaboratorHandler : IRequestHandler<RemoveProjectCollaboratorCommand, string>
    {

        private readonly IAppDbContext _context;

        public RemoveProjectCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(RemoveProjectCollaboratorCommand request, CancellationToken cancellationToken)
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
