using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using ProjectManager.Application.interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.ProjectCollaborator.Queries
{
    public class GetCollaboratorRecomandationQuerry : IRequest<IEnumerable<CollaboratorRecomandationDto>>
    {
        public int ProjectId { get; set; }
        public string Search { get; set; }
    }

    public class GetCollaboratorRecomandationHandler : IRequestHandler<GetCollaboratorRecomandationQuerry, IEnumerable<CollaboratorRecomandationDto>>
    {
        private readonly IAppDbContext _context;
        public GetCollaboratorRecomandationHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CollaboratorRecomandationDto>> Handle(GetCollaboratorRecomandationQuerry request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .Where(u => u.Role.Name == "user" && u.UserName.ToLower().StartsWith(request.Search.ToLower())).ToListAsync();

            var response = users.Select(u => new CollaboratorRecomandationDto
            {
                Id = u.Id,
                UserName = u.UserName
            }).ToList();

            return response;
        }
    }

}
