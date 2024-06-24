using MediatR;
using ProjectManager.Application.DataTransferObjects.User;
using ProjectManager.Application.interfaces;
using ProjectManager.Application.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager.Application.UserManagement.UpdateUserByClient
{
    public class UpdateUserByClientCommand : IRequest<string>
    {
        public UserByIdForClientDto User { get; set; }
    }

    public class UpdateUserByClientHandler : IRequestHandler<UpdateUserByClientCommand, string>
    {
        private readonly IAppDbContext _context;
        private readonly IFileService _fileService;

        public UpdateUserByClientHandler(IAppDbContext context, IFileService fileService)
        {
            _context = context;
            _fileService = fileService;
        }

        public async Task<string> Handle(UpdateUserByClientCommand request, CancellationToken cancellationToken)
        {
            var userToUpdate = await _context.Users.FindAsync(request.User.Id);

            if (userToUpdate == null)
            {
                return "Coudn't find the user";
            }

            var isUsernameAvalaible = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.User.Username && u.Id != request.User.Id);

            if (isUsernameAvalaible != null)
            {
                return "Username it's not avalaible";
            }

            string newFilePath = await _fileService.GetPhotoPath(request.User.Photo, userToUpdate.PhotoPath, request.User.RemovePhoto, true);

            userToUpdate.PhotoPath = newFilePath;

            userToUpdate.UserName = request.User.Username;
            userToUpdate.FirstName = request.User.FirstName;
            userToUpdate.LastName = request.User.LastName;

            try
            {
                _context.Users.AddOrUpdate(userToUpdate);
                await _context.SaveAsync(cancellationToken);
                return "success";
            }
            catch
            {
                return "Can't update the user. A problem occured on the server";
            }

        }
    }
}
