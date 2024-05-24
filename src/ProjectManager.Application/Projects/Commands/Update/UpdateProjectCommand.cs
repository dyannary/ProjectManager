using MediatR;
using ProjectManager.Application.DTOs.Projects;
using ProjectManager.Application.interfaces;
using ProjectManager.Domain.Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.Projects.Commands.Update
{
    public class UpdateProjectCommand : IRequest<bool>
    {
        public ProjectByIdDto Project { get; set; }
        public int UserID { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
    {

        private readonly IAppDbContext _context;
        public UpdateProjectHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {

            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == 1);

            var projectToUpdate = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Project.Id);

            projectToUpdate.Name = request.Project.Name;
            projectToUpdate.Description = request.Project.Description;
            projectToUpdate.IsDeleted = request.Project.IsDeleted;
            projectToUpdate.ProjectStartDate = request.Project.ProjectStartDate;
            projectToUpdate.ProjectEndDate = request.Project.ProjectEndDate;
            projectToUpdate.LastModified = DateTime.Now;
            projectToUpdate.LastModifiedBy = request.UserID;
            projectToUpdate.ProjectState = projectState;

            _context.Projects.AddOrUpdate(projectToUpdate);

            if (await _context.SaveAsync(cancellationToken) == 1)
                return true;
            else return false;
        }
    }

}
