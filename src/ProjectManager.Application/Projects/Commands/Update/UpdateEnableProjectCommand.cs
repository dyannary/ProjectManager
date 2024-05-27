using MediatR;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Update
{
    public class UpdateEnableProjectCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class UpdateEnableProjectCommandHandler : IRequestHandler<UpdateEnableProjectCommand, bool>
    {

        private readonly IAppDbContext _context;
        public UpdateEnableProjectCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateEnableProjectCommand request, CancellationToken cancellationToken)
        {

            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id.Equals(request.Id));

            if (project == null)
                return false;

            if (project.IsDeleted)
                project.IsDeleted = false;
            else 
                project.IsDeleted = true;

            _context.Projects.AddOrUpdate(project);

            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else return false;
        }
    }
}
