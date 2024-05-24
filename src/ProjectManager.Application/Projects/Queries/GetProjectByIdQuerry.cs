using MediatR;
using ProjectManager.Application.DTOs.Projects;
using ProjectManager.Application.interfaces;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectByIdQuerry : IRequest<ProjectByIdDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuerry, ProjectByIdDto>
    {

        private readonly IAppDbContext _context;
        public GetProjectByIdHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectByIdDto> Handle(GetProjectByIdQuerry request, CancellationToken cancellationToken)
        {
            var querry = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Id);

            var response = new ProjectByIdDto
            {
                Id = querry.Id,
                Name = querry.Name,
                Description = querry.Description,
                IsDeleted = querry.IsDeleted,
                PhotoPath = querry.PhotoPath,
                ProjectEndDate = querry.ProjectEndDate,
                ProjectStartDate = querry.ProjectStartDate,
            };

            return response;
        }
    }

}
