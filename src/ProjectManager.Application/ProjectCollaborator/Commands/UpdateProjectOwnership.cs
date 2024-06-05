using MediatR;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Commands
{
    public class TransferProjectToCollaboratorCommand : IRequest<bool>
    {
        public string CollaboratorUserName { get; set; }
        public int ProjectId { get; set; }
        public int OwnerId { get; set; }
    }

    public class TransferProjectToCollaboratorHandler : IRequestHandler<TransferProjectToCollaboratorCommand, bool>
    {
        private readonly IAppDbContext _context;

        public TransferProjectToCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(TransferProjectToCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var newOwnerId = await _context.Users.FirstOrDefaultAsync(n => n.UserName == request.CollaboratorUserName);
            var CreatorRole = await _context.UserProjectRole.FirstOrDefaultAsync(r => r.Name == "ProjectCreator");
            var UserRole = await _context.UserProjectRole.FirstOrDefaultAsync(r => r.Name == "User");


            var userProjectForOwner = await _context.UserProjects.FirstOrDefaultAsync(u => u.UserId == request.OwnerId && u.ProjectId == request.ProjectId); 
            var userProjectForNewOwner = await _context.UserProjects.FirstOrDefaultAsync(u => u.UserId == newOwnerId.Id && u.ProjectId == request.ProjectId);

            userProjectForOwner.UserProjectRole = UserRole;
            userProjectForNewOwner.UserProjectRole = CreatorRole;


            _context.UserProjects.AddOrUpdate(userProjectForOwner);
            _context.UserProjects.AddOrUpdate(userProjectForNewOwner);

            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else return false; 
        }
    }

}
