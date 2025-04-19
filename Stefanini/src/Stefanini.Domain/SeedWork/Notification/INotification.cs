using static Stefanini.Domain.SeedWork.NotificationModel;

namespace Stefanini.Domain.SeedWork.Notification
{
    public interface INotification
    {
        NotificationModel NotificationModel { get; }
        bool HasNotification { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
    }
}
