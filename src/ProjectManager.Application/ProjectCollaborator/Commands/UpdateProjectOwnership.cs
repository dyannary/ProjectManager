using MediatR;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Commands
{
    public class TransferProjectToCollaboratorCommand : IRequest<string>
    {
        public string CollaboratorUserName { get; set; }
        public int ProjectId { get; set; }
        public int OwnerId { get; set; }
    }

    public class TransferProjectToCollaboratorHandler : IRequestHandler<TransferProjectToCollaboratorCommand, string>
    {
        private readonly IAppDbContext _context;

        public TransferProjectToCollaboratorHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(TransferProjectToCollaboratorCommand request, CancellationToken cancellationToken)
        {
            var newOwnerId = await _context.Users.FirstOrDefaultAsync(n => n.UserName == request.CollaboratorUserName);
            var CreatorRole = await _context.UserProjectRole.FirstOrDefaultAsync(r => r.Name == "ProjectCreator");
            var UserRole = await _context.UserProjectRole.FirstOrDefaultAsync(r => r.Name == "User");


            var userProjectForOwner = await _context.UserProjects.FirstOrDefaultAsync(u => u.UserId == request.OwnerId && u.ProjectId == request.ProjectId); 
            var userProjectForNewOwner = await _context.UserProjects.FirstOrDefaultAsync(u => u.UserId == newOwnerId.Id && u.ProjectId == request.ProjectId);

            if (newOwnerId == null || CreatorRole == null || UserRole == null || userProjectForOwner == null || userProjectForNewOwner == null)
            {
                return "The server couldn't procces the data. Try again!";
            }

            userProjectForOwner.UserProjectRole = UserRole;
            userProjectForNewOwner.UserProjectRole = CreatorRole;

            try
            {
                _context.UserProjects.AddOrUpdate(userProjectForOwner);
                _context.UserProjects.AddOrUpdate(userProjectForNewOwner);
                await _context.SaveAsync(cancellationToken);
                return "success";
            }
            catch
            {
                return "A problem occured on the server. Try again!";
            }
        }
    }

}
