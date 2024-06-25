using ProjectManager.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace ProjectManager.Application.Services
{
    public class FileService : IFileService
    {
        private readonly string _defaultPath = $"/Content/Images";

        public async Task<string> SaveFile(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                return await UploadPhoto(file, _defaultPath);
            else
                return _defaultPath;
        }

        public async Task<string> UpdateFile(HttpPostedFileBase file, string path)
        {
            string pathMapped = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(pathMapped))
            {
                if (Path.GetFileName(path) != "defaultImage.jpg" && Path.GetFileName(path) != "default_avatar.jpg")
                    File.Delete(pathMapped);
            }

            return await UploadPhoto(file, _defaultPath);
        }

        public bool RemoveFile(string path)
        {
            string pathMapped = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(pathMapped))
            {
                try
                {
                    if (Path.GetFileName(path) != "defaultImage.jpg" && Path.GetFileName(path) != "default_avatar.jpg")
                        File.Delete(pathMapped);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }

        #region private methods
        private async Task<string> UploadPhoto(HttpPostedFileBase file, string path)
        {
            string pathMapped = HttpContext.Current.Server.MapPath(path);
            string filename = Path.GetFileName(file.FileName);
            string savePath = Path.Combine(pathMapped, filename);

            if (File.Exists(savePath))
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid().ToString() + fileExtension;
                savePath = Path.Combine(pathMapped, newFileName);
            }

            try
            {
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await file.InputStream.CopyToAsync(fileStream);
                }

                return $"{path}/{Path.GetFileName(savePath)}";

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public async Task<string> GetPhotoPath(HttpPostedFileBase file, string path, bool isRemoved, bool user = false)
        {
            if(isRemoved)
                if (user)
                    return $"{_defaultPath}/Default/default_avatar.jpg";
                else 
                    return $"{_defaultPath}/Default/defaultImage.jpg";

            return file == null ? null : await UpdateFile(file, path);

        }

        #endregion
    }
}
