using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.DTOs.User
{
    public class UserByIdDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
