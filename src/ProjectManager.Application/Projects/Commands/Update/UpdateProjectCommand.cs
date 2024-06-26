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
    public class UpdateProjectCommand : IRequest<string>
    {
        public ProjectByIdDto Project { get; set; }
        public int UserID { get; set; }
    }

    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, string>
    {

        private readonly IAppDbContext _context;
        private readonly IFileService _fileService;
        public UpdateProjectHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<string> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {

            var projectState = await _context.ProjectStates.FirstOrDefaultAsync(ps => ps.Id == request.Project.ProjectStateID);

            var projectToUpdate = await _context.Projects.FirstOrDefaultAsync(p => p.Id == request.Project.Id);

            var path = projectToUpdate.PhotoPath;

            if (projectState == null || projectToUpdate == null)
            {
                return "The server couldn't proccess the data!";
            }

            string photoPath;

            if (request.Project.File != null || request.Project.RemoveFile == true)
            {
                try
                {
                    var isRemoved = request.Project.RemoveFile;
                    HttpPostedFileBase file = request.Project.File;
                    photoPath = await _fileService.GetPhotoPath(file, path, isRemoved);
                }
                catch
                {
                    return "Couldn't upload the image, try again!";
                }
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

            try
            {
                _context.Projects.AddOrUpdate(projectToUpdate);
                await _context.SaveAsync(cancellationToken);
                return "success";
            }
            catch
            {
                return "A problem occured on the server!";
            }
        }
    }
}
