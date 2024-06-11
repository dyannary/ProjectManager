using MediatR;
using ProjectManager.Application.DataTransferObjects.ProjectTask.CollaboratorsManagement;
using ProjectManager.Application.interfaces;
using System.Threading.Tasks;
using System.Threading;
using System.Data.Entity;
using System.Linq;
using ProjectManager.Application.DataTransferObjects.ProjectCollaborator;
using System;
using System.Collections.Generic;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectCollaboratorsQuerry : IRequest<CollaboratorsResponseDto>
    {
        public int ProjectId { get; set; }
        public string Search { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int LoggedUserId { get; set; }
    }

    public class GetProjectCollaboratorsHandler : IRequestHandler<GetProjectCollaboratorsQuerry, CollaboratorsResponseDto>
    {
        private readonly IAppDbContext _context;
        public GetProjectCollaboratorsHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<CollaboratorsResponseDto> Handle(GetProjectCollaboratorsQuerry request, CancellationToken cancellationToken)
        {
            var loggedUserForProjectRole = await _context.UserProjects.FirstOrDefaultAsync(up => up.UserId == request.LoggedUserId && up.ProjectId == request.ProjectId);
            if (loggedUserForProjectRole == null)
            {
                return null;
            }

            if (loggedUserForProjectRole.UserProjectRole.Name == "User")
                return new CollaboratorsResponseDto
                {
                    ProjectId = request.ProjectId,
                    MaxPage = 1,
                    FromPage = 1,
                    CurrentPage = 1,
                    LoggedUserRole = loggedUserForProjectRole.UserProjectRole.Name,
                    CollaboratorsDetails = new List<CollaboratorsDetailsDto>()
                };

            var projectCreator = await _context.UserProjects.FirstOrDefaultAsync(u => u.UserProjectRole.Name == "ProjectCreator" && u.ProjectId == request.ProjectId);

            var querry = _context.Users.Where(u => u.UserProjects.Any(up => up.ProjectId == request.ProjectId && up.UserId != request.LoggedUserId && up.UserId != projectCreator.UserId))
                .Where(u => u.Role.Name == "user")
                .AsQueryable();

            #region pagination and filters
            if (!string.IsNullOrEmpty(request.Search))
                {
                    string search = request.Search.ToLower().Trim();
                    querry = querry.Where(q => q.UserName.ToLower().Contains(search));
                }

            querry = querry.OrderByDescending(q => q.Id);

            var details = await querry
              .Skip((request.Page - 1) * request.PageSize)
              .Take(request.PageSize)
              .ToListAsync(cancellationToken);

            int detailsCount = await querry.CountAsync(cancellationToken);
            int pageCount = (int)Math.Ceiling((double)detailsCount / request.PageSize);

            if (request.Page + 2 <= pageCount)
                pageCount = request.Page + 2;

            int fromPage = 1;
            if (request.Page - 2 >= 1)
                fromPage = request.Page - 2;

            #endregion

            var responseDetails = details.Select(r => new CollaboratorsDetailsDto
            {
                Id = r.Id,
                UserName = r.UserName,
                CollaboratorProjectRole = r.UserProjects.FirstOrDefault(up => up.ProjectId == request.ProjectId).UserProjectRole.Name
            }).ToList();

            var response = new CollaboratorsResponseDto
            {
                CollaboratorsDetails = responseDetails,
                FromPage = fromPage,
                MaxPage = pageCount,
                LoggedUserRole = loggedUserForProjectRole.UserProjectRole.Name,
                CurrentPage = request.Page,
                ProjectId = request.ProjectId,
                LoggedUserName = loggedUserForProjectRole.User.UserName,
                ProjectCreatorName = projectCreator.User.UserName,
            };

            return response;
        }
    }
}
