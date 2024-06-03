using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectByIdForProjectTaskQuerry : IRequest<ProjectByIdForProjectTaskDto>
    {
        public int Id { get; set; } 
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
            var querry = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Id);

            var response = new ProjectByIdForProjectTaskDto
            {
                Id = querry.Id,
                Name = querry.Name,
                Description = querry.Description,
                PhotoPath = querry.PhotoPath,
                IsDeleted = querry.IsDeleted,
                ProjectEndDate = querry.ProjectEndDate,
                ProjectStartDate = querry.ProjectStartDate,
                ProjectStateID = querry.ProjectStateId
            };

            return response;

        }
    }

}
