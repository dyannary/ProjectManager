using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask;
using ProjectManager.Application.interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectUsersQuery : IRequest<IEnumerable<TaskDropdownsDto>>
    {
        public int Id { get; set; }
    }

    public class GetProjectUsersQueryHandler : IRequestHandler<GetProjectUsersQuery, IEnumerable<TaskDropdownsDto>>
    {
        private readonly IAppDbContext _context;
        public GetProjectUsersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskDropdownsDto>> Handle(GetProjectUsersQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects.Include(pt => pt.UserProjects).FirstOrDefaultAsync(p => p.Id == request.Id);

            var users = projects.UserProjects.Select(user => new TaskDropdownsDto
            {
                Id = user.UserId,
                Name = user.User.UserName
            });

            return users;
        }
    }
}
