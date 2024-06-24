using System.Web;

namespace ProjectManager.Application.DataTransferObjects.User
{
    public class UserByIdForClientDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public bool RemovePhoto { get; set; }
    }
}
