using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.DTOs.User
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
