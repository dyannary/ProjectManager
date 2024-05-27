using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Queries
{
    public class GetProjectsByFilterQuery : IRequest<List<CardDto>>
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string Owning { get; set; }
        public DateTime StartDate {  get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetProjectsByFilterHandler : IRequestHandler<GetProjectsByFilterQuery, List<CardDto>>
    {
        private readonly IAppDbContext _context;
        public GetProjectsByFilterHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CardDto>> Handle(GetProjectsByFilterQuery request, CancellationToken cancellationToken)
        {
            var projects = _context.Projects.AsQueryable()
                .OrderBy(p => p.Name)
                .Include(ps => ps.ProjectState)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize);

            if (request.Name != null && request.Name != "")
            {
                string name = request.Name.Replace("", " ").ToLower();

                projects = projects.Where(p => p.Name.Replace("", " ").ToLower() == name);
            }

            var cardDtos = projects.Select(p => new CardDto
            {
                Name = p.Name,
                Description = p.Description,
                photoPath = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/2c/Default_pfp.svg/2048px-Default_pfp.svg.png"
            }).ToList();
 
            return cardDtos;
        }
    }

}
