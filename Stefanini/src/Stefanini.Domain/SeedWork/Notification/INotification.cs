using static Stefanini.Domain.SeedWork.Notification.NotificationModel;

namespace Stefanini.Domain.SeedWork.Notification
{
    public interface INotification
    {
        List<NotificationModel> ListNotificationModel { get; }
        bool HasNotification { get; }
        void AddNotification(string key, string message, ENotificationType notificationType);
    }
}
