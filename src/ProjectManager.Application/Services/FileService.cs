using ProjectManager.Application.Enums;
using ProjectManager.Application.Interfaces;
using System;
using System.IO;
using System.Web;

namespace ProjectManager.Application.Services
{
    public class FileService : IFileService
    {
        public string SaveFile(HttpPostedFileBase file, FileTypeEnum type)
        {
            string path = "";
            #region GetPath
            if (type == FileTypeEnum.Default)
                return "~/Content/Default/defaultImage.jpg";

            else if (type == FileTypeEnum.Image)
            {
                if (IsImage(file))
                    path = "~/Content/Images";
                else
                    return "~/Content/Default/defaultImage.jpg";
            }
            else
                path = "~/Content/Files";
            #endregion

            if (file != null && file.ContentLength > 0)
                return UploadPhoto(file, path);
            else
            return "~/Content/Images/Default/defaultImage.jpg";
        }

        public string UpdateFile(HttpPostedFileBase file, FileTypeEnum type, string path)
        {
            string pathMapped = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(pathMapped))
            {
                if (Path.GetFileName(path) != "defaultImage.jpg")
                    File.Delete(pathMapped);
            }

            #region GetPath
            if (type == FileTypeEnum.Default)
                return "~/Content/Images/Default/defaultImage.jpg";

            else if (type == FileTypeEnum.Image)
            {
                if (IsImage(file))
                    path = "~/Content/Images";
                else
                    return "~/Content/Default/defaultImage.jpg";
            }
            else
                path = "~/Content/Files";
            #endregion

            if (file != null && file.ContentLength > 0)
            {
                return UploadPhoto(file, path);
            }
            else
                return "~/Content/Default/defaultImage.jpg";


        }

        public bool RemoveFile(string path)
        {
            string pathMapped = HttpContext.Current.Server.MapPath(path);

            if (File.Exists(pathMapped))
            {
                try
                {
                    File.Delete(pathMapped);
                    return true;
                }
                catch
                {
                    return false;
                }
            } else 
                return false;
        }

        #region private methods
        private string UploadPhoto(HttpPostedFileBase file, string path)
        {

            string pathMapped = HttpContext.Current.Server.MapPath(path);
            string filename = Path.GetFileName(file.FileName);
            string savePath = Path.Combine(pathMapped, filename);

            if (File.Exists(savePath))
            {
                string fileNameWithouExtension = Path.GetFileNameWithoutExtension(file.FileName);
                string fileExtension = Path.GetExtension(file.FileName);
                string newFileName = fileNameWithouExtension + "-" + Guid.NewGuid().ToString() + fileExtension;
                savePath = Path.Combine(pathMapped, newFileName);
            }

            file.SaveAs(savePath);
            return path + "/" + file.FileName;
        }

        public bool IsImage(HttpPostedFileBase file)
        {

            string fileExtension = Path.GetExtension(file.FileName);

            try
            {
                if (fileExtension.Equals(".jpg") || fileExtension.Equals(".png") || fileExtension.Equals(".jpeg") || fileExtension.Equals(".gif"))
                    return true;
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion


    }
}
