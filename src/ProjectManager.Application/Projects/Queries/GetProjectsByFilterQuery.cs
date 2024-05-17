using MediatR;
using ProjectManager.Application.DTOs.User;
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
        public string name { get; set; }
        public string Status { get; set; }
        public string Owning { get; set; }
        public DateTime startDate {  get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
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
                .Skip((request.page - 1) * request.pageSize)
                .Take(request.pageSize);

            if (request.name != null && request.name != "")
            {
                string name = request.name.Replace("", " ").ToLower();

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
