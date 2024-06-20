using System.Threading.Tasks;
using System.Web;

namespace ProjectManager.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> GetPhotoPath(HttpPostedFileBase file, string path, bool isRemoved);
        Task<string> SaveFile(HttpPostedFileBase file);
        Task<string> UpdateFile(HttpPostedFileBase file, string path);
        bool RemoveFile(string path);
    }
}
