using ProjectManager.Application.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.Application.Services
{
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            password = HashPassword(password);
            return password.Equals(hashedPassword);
        }
    }
}
