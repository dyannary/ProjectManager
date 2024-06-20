using MediatR;
using ProjectManager.Application.DataTransferObjects.Projects;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
        private readonly IFileService _fileService;
        public UpdateProjectHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {

            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == request.Project.ProjectStateID);

            var projectToUpdate = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Project.Id);

            var path = projectToUpdate.PhotoPath;

            string photoPath;

            if (request.Project.File != null || request.Project.RemoveFile == true)
            {
                var isRemoved = request.Project.RemoveFile;

                HttpPostedFileBase file = request.Project.File;

                photoPath = await _fileService.GetPhotoPath(file, path, isRemoved);
            }
            else
                photoPath = path;   

            projectToUpdate.Name = request.Project.Name;
            projectToUpdate.Description = request.Project.Description;
            projectToUpdate.IsDeleted = request.Project.IsDeleted;
            projectToUpdate.ProjectStartDate = request.Project.ProjectStartDate;
            projectToUpdate.PhotoPath = photoPath;
            projectToUpdate.ProjectEndDate = request.Project.ProjectEndDate;
            projectToUpdate.LastModified = DateTime.Now;
            projectToUpdate.LastModifiedBy = request.UserID;
            projectToUpdate.ProjectState = projectState;

            _context.Projects.AddOrUpdate(projectToUpdate);

            try
            {
                await _context.SaveAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
