namespace ProjectManager.Application.Interfaces
{
    public interface IPasswordEncryptionService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);   
    }
}
