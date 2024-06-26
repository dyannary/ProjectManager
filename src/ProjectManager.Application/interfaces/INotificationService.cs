using System.Threading.Tasks;

namespace ProjectManager.Application.Services
{
    public interface INotificationService
    {
        Task NotifyAsync(int count, string nameIdentifier);
    }
}
