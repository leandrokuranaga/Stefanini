using System.Diagnostics.CodeAnalysis;

namespace Stefanini.Domain.SeedWork.Notification
{
    [ExcludeFromCodeCoverage]
    public class NotificationModel(string key, string message, NotificationModel.ENotificationType notificationType = NotificationModel.ENotificationType.BusinessRules)
    {
        public Guid NotificationId { get; private set; } = Guid.NewGuid();
        public string Key { get; private set; } = key;
        public string Message { get; private set; } = message;
        public ENotificationType NotificationType { get; set; } = notificationType;

        public void UpdateMessage(string message, string key)
        {
            Message = message;
            Key = key;
        }

        public enum ENotificationType : byte
        {
            Default = 0,
            InternalServerError = 1,
            BusinessRules = 2,
            NotFound = 3,
            BadRequestError = 4,
        }
    }
}
