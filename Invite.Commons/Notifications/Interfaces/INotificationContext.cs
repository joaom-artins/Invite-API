namespace Invite.Commons.Notifications.Interfaces;

public interface INotificationContext
{
    int StatusCode { get; }
    string Title { get; }
    string Detail { get; }
    IDictionary<string, string[]> Notifications { get; }
    bool HasNotifications { get; }
    bool IsEmptyNotificationList { get; }

    void AddNotification(string field, string message);
    void SetDetails(int statusCode, string title, string detail);
}
