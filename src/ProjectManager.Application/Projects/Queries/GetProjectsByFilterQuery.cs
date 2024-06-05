using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectsByFilterQuery : IRequest<ProjectFilterResponse>
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string IsDeleted {  get; set; }
        public string Owning { get; set; }
        public DateTime StartDate {  get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int UserID { get; set; }
    }

    public class GetProjectsByFilterHandler : IRequestHandler<GetProjectsByFilterQuery, ProjectFilterResponse>
    {
        private readonly IAppDbContext _context;
        public GetProjectsByFilterHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectFilterResponse> Handle(GetProjectsByFilterQuery request, CancellationToken cancellationToken)
        {
            var querry = _context.Projects
            .Where(p => p.UserProjects.Any(up => up.UserId == request.UserID))
            .AsQueryable()
            .OrderByDescending(p => p.Id)
            .Include(ps => ps.ProjectState);

            if (!string.IsNullOrEmpty(request.Name))
            {
                string name = request.Name.ToLower().Trim();

                querry = querry.Where(q => q.Name.ToLower().Contains(name));
            }

            if (request.Owning != null && request.Owning != "all")
            {
                if (request.Owning == "myprojects") 
                {
                    querry = querry.Where(p => p.UserProjects.Any(up => up.UserProjectRole.Name == "ProjectCreator" && up.UserId == request.UserID));
                }
                else 
                    querry = querry.Where(p => p.UserProjects.Any(up => up.UserProjectRole.Name != "ProjectCreator" && up.UserId == request.UserID));
            }

            if (request.Status != null && request.Status != "all")
            {
                if (request.Status == "completed")
                    querry = querry.Where(q => q.ProjectEndDate <= DateTime.Now);
                else
                    querry = querry.Where(q => q.ProjectEndDate >= DateTime.Now);
            }

            if (request.IsDeleted != null && request.IsDeleted!= "all")
            {
                if (request.IsDeleted == "deactivated")
                    querry = querry.Where(q => q.IsDeleted == true);
                else querry = querry.Where(q => q.IsDeleted == false);
            }

            var projects = await querry
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            int projectCount = await querry.CountAsync(cancellationToken);
            int pageCount = (int)Math.Ceiling((double)projectCount / request.PageSize);

            if (request.Page + 2 <= pageCount)
                pageCount = request.Page + 2;

            int fromPage = 1;
            if (request.Page - 2 >= 1)
                fromPage = request.Page - 2;


            var cardDtos = projects.Select(p => new CardDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                PhotoPath = p.PhotoPath,
                IsEnabled = p.IsDeleted,
                Status = p.ProjectState.Name,
            }).ToList();

            var response = new ProjectFilterResponse
            {
                Cards = cardDtos,
                MaxPage = pageCount,
                FromPage = fromPage
            };

            return response;
        }
    }

}
