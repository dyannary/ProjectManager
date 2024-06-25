using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.DataTransferObjects.User
{
    public class UserByIdDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
