using ProjectManager.Domain.Entities;

namespace ProjectManager.Application.DataTransferObjects.User
{
    public class UserByUsernameAndPasswordDto
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsEnabled { get; set; }
        public string Role { get; set; }
    }
}
