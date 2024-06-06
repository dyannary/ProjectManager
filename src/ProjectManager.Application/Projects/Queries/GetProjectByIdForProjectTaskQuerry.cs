using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectByIdForProjectTaskQuerry : IRequest<ProjectByIdForProjectTaskDto>
    {
        public int Id { get; set; }
        public int LoggedUserId { get; set; }
    }

    public class GetProjectByIdForProjectTaskHandler : IRequestHandler<GetProjectByIdForProjectTaskQuerry, ProjectByIdForProjectTaskDto>
    {
        IAppDbContext _context;
        public GetProjectByIdForProjectTaskHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectByIdForProjectTaskDto> Handle(GetProjectByIdForProjectTaskQuerry request, CancellationToken cancellationToken)
        {
            var loggedUser = await _context.UserProjects.FirstOrDefaultAsync(u => u.ProjectId == request.Id && u.UserId == request.LoggedUserId);
            var owner = await _context.UserProjects.FirstOrDefaultAsync(p => p.ProjectId == request.Id && p.UserProjectRole.Name == "ProjectCreator");

            var querry = await _context.Projects
                .Include(p => p.ProjectState)
                .Where(p => p.UserProjects.Any(up => up.UserId == request.LoggedUserId))
                .FirstOrDefaultAsync(p => p.Id == request.Id);

            var response = new ProjectByIdForProjectTaskDto
            {
                Id = querry.Id,
                Name = querry.Name,
                Description = querry.Description,
                PhotoPath = querry.PhotoPath,
                IsDeleted = querry.IsDeleted,
                ProjectEndDate = querry.ProjectEndDate,
                ProjectStartDate = querry.ProjectStartDate,
                ProjectState = querry.ProjectState.Name,
                LoggedUserRole = loggedUser.UserProjectRole.Name,
                OwnerUserName = owner.User.UserName
            };

            return response;

        }
    }

}
