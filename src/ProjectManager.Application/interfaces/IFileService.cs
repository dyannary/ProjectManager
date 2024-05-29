using ProjectManager.Application.Enums;
using System.Web;

namespace ProjectManager.Application.Interfaces
{
    public interface IFileService
    {
         string SaveFile(HttpPostedFileBase file, FileTypeEnum type);
         string UpdateFile(HttpPostedFileBase file, FileTypeEnum type, string path);
         bool RemoveFile(string path);
    }
}
